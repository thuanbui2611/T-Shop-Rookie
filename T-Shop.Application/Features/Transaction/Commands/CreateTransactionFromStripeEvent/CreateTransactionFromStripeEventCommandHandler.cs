using AutoMapper;
using MediatR;
using Stripe;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
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

    public CreateTransactionFromStripeEventCommandHandler(IOrderQueries orderQueries, IUnitOfWork unitOfWork, ITransactionQueries transactionQueries, IMapper mapper)
    {
        _orderQueries = orderQueries;
        _unitOfWork = unitOfWork;
        _transactionRepository = unitOfWork.GetBaseRepo<Domain.Entity.Transaction>();
        _cartItemRepository = unitOfWork.GetBaseRepo<CartItem>();
        _cartRepository = unitOfWork.GetBaseRepo<Domain.Entity.Cart>();
        _transactionQueries = transactionQueries;
        _mapper = mapper;
    }

    public async Task<TransactionResponseModel> Handle(CreateTransactionFromStripeEventCommand request, CancellationToken cancellationToken)
    {
        var charge = (Charge)request.stripeEvent.Data.Object;
        if (charge.Status == "succeeded")
        {
            await CreateNewTransaction(charge);
            var transaction = await _transactionQueries.GetTransactionByPaymentIntentId(charge.PaymentIntentId);
            var result = _mapper.Map<TransactionResponseModel>(transaction);
            return result;
        }
        return null;
    }

    private async Task CreateNewTransaction(Charge charge)
    {
        var order = await _orderQueries.GetOrderByPaymentIntentIdAsync(charge.PaymentIntentId);
        //Add new transaction
        var transaction = new Domain.Entity.Transaction
        {
            CustomerID = order.UserID,
            OrderID = order.Id,
            Status = TransactionStatusConstants.PENDING,
        };
        _transactionRepository.Add(transaction);

        //Delete cart items
        var productInCartToDelete = new List<CartItem>();
        foreach (var orderDetail in order.OrderDetails)
        {
            //Find cart to delete cart items
            var cart = await _cartRepository.FindOne(c => c.UserID.Equals(order.UserID));
            var cartItem = new CartItem();
            if (cart != null)
            {
                //Find cart item to delete
                cartItem = await _cartItemRepository.FindOne(ci =>
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
}
