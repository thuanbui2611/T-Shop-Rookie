using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System.Text.RegularExpressions;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Infrastructure.SharedServices.EmailService;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Commands.CreateTransactionFromStripeEvent;
public class CreateTransactionFromStripeEventCommandHandler : IRequestHandler<CreateTransactionFromStripeEventCommand, TransactionResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderQueries _orderQueries;
    private readonly ITransactionQueries _transactionQueries;

    private readonly IGenericRepository<Domain.Entity.Transaction> _transactionRepository;
    private readonly IGenericRepository<CartItem> _cartItemRepository;
    private readonly IGenericRepository<Domain.Entity.Cart> _cartRepository;
    private readonly IGenericRepository<Domain.Entity.Order> _orderRepository;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    public CreateTransactionFromStripeEventCommandHandler(IOrderQueries orderQueries, IUnitOfWork unitOfWork, ITransactionQueries transactionQueries, IMapper mapper, UserManager<ApplicationUser> userManager, IEmailService emailService)
    {
        _orderQueries = orderQueries;
        _unitOfWork = unitOfWork;
        _transactionRepository = unitOfWork.GetBaseRepo<Domain.Entity.Transaction>();
        _cartItemRepository = unitOfWork.GetBaseRepo<CartItem>();
        _cartRepository = unitOfWork.GetBaseRepo<Domain.Entity.Cart>();
        _orderRepository = unitOfWork.GetBaseRepo<Domain.Entity.Order>();
        _transactionQueries = transactionQueries;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<TransactionResponseModel> Handle(CreateTransactionFromStripeEventCommand request, CancellationToken cancellationToken)
    {
        var charge = (Charge)request.stripeEvent.Data.Object;
        if (charge.Status == "succeeded")
        {
            await CreateNewTransaction(charge);
            var transaction = await _transactionQueries.GetTransactionByPaymentIntentId(charge.PaymentIntentId);
            var customer = await _userManager.FindByIdAsync(transaction.CustomerID.ToString());

            await SendEmailToCustomersAsync(transaction, customer);

            var result = _mapper.Map<TransactionResponseModel>(transaction);
            return result;
        }
        return null;
    }

    private async Task CreateNewTransaction(Charge charge)
    {
        var order = await _orderQueries.GetOrderByPaymentIntentIdAsync(charge.PaymentIntentId);
        //Update order status
        order.IsPayment = true;
        //Update quantity of product
        foreach (var item in order.OrderDetails)
        {
            item.Product.Quantity = item.Product.Quantity - item.Quantity;
        }

        _orderRepository.Update(order);
        //Add new transaction
        var transaction = new Domain.Entity.Transaction
        {
            CustomerID = order.UserID,
            OrderID = order.Id,
            Status = TransactionConstants.PENDING,
        };
        _transactionRepository.Add(transaction);

        //Delete cart items
        var productInCartToDelete = new List<CartItem>();
        foreach (var orderDetail in order.OrderDetails)
        {
            //Find cart to delete cart items
            var cart = await _cartRepository.FindOne(c => c.UserID.Equals(order.UserID));
            if (cart != null)
            {
                //Find cart item to delete
                var cartItem = await _cartItemRepository.FindOne(ci =>
                            ci.ProductID.Equals(orderDetail.ProductID)
                            && ci.CartID.Equals(cart.Id));
                if (cartItem != null)
                {
                    productInCartToDelete.Add(cartItem);
                }
            }
        }
        if (productInCartToDelete.Count > 0)
        {
            _cartItemRepository.DeleteRange(productInCartToDelete);
        }
        await _unitOfWork.CompleteAsync();
    }

    private async Task SendEmailToCustomersAsync(Domain.Entity.Transaction transaction, ApplicationUser customer)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "newOrderFromCustomer.html");
        using (StreamReader reader = new StreamReader(filePath))
        {
            string content = await reader.ReadToEndAsync();
            content = Regex.Replace(content, "###ORDER_ID###", transaction.Id.ToString());
            content = Regex.Replace(content, "###NUM_ITEM_PURCHASED###", transaction.Order.OrderDetails.Count().ToString());
            content = Regex.Replace(content, "###TOTAL_PAYMENT###", transaction.Order.OrderDetails.Sum(o => o.Price * o.Quantity).ToString());
            content = Regex.Replace(content, "###SHIPPING_ADDRESS###", transaction.Order.ShippingAddress);
            content = Regex.Replace(content, "###ESTIMATED_DELIVERY_DATE###", DateTime.Now.AddDays(3).ToShortDateString());
            var emailOptions = new SendEmailOptions
            {
                Subject = "Payment Bill",
                Body = content,
                ToEmail = customer.Email,
                ToName = customer.FullName,
            };

            await _emailService.SendEmailAsync(emailOptions);
        }
    }
}
