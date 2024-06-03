using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Infrastructure.SharedServices.EmailService;
using T_Shop.Infrastructure.SharedServices.StripeService;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Commands.UpdateStatusTransaction;
public class UpdateStatusTransactionCommandHandler : IRequestHandler<UpdateStatusTransactionCommand, TransactionResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionQueries _transactionQueries;
    private readonly IStripeService _stripeService;

    private readonly IGenericRepository<Domain.Entity.Transaction> _transactionRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    public UpdateStatusTransactionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITransactionQueries transactionQueries, IStripeService stripeService, UserManager<ApplicationUser> userManager, IEmailService emailService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _transactionRepository = unitOfWork.GetBaseRepo<Domain.Entity.Transaction>();
        _transactionQueries = transactionQueries;
        _stripeService = stripeService;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<TransactionResponseModel> Handle(UpdateStatusTransactionCommand request, CancellationToken cancellationToken)
    {
        //Validate status updated
        var isValid = TransactionConstants.AVAILABLE_UPDATE_TRANSACTION_STATUS.Contains(request.Status);
        if (!isValid) throw new BadRequestException($"Only allow update to status: " +
            $"{String.Join(", ", TransactionConstants.AVAILABLE_UPDATE_TRANSACTION_STATUS)}");

        //Validate request
        var transaction = await _transactionQueries.GetTransactionsByIdAsync(request.ID, true);

        if (transaction == null) throw new BadRequestException("Transaction not found");

        if (request.Status.Equals(TransactionConstants.CANCELED))
        {
            await HandleRefundTransaction(transaction, request.Reason);
        }
        //update status of transaction
        transaction.Status = request.Status;
        if (!request.Reason.IsNullOrEmpty())
        {
            transaction.Reason = request.Reason;
        }
        _transactionRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<TransactionResponseModel>(transaction);
        return result;
    }

    private async Task<bool> HandleRefundTransaction(Domain.Entity.Transaction transaction, string reason)
    {
        var totalPayment = transaction.Order.OrderDetails.Sum(od => od.Price * od.Quantity);
        var refund = await _stripeService.RefundPayment(transaction.Order.PaymentIntentID, totalPayment, reason);

        var customer = await _userManager.FindByIdAsync(transaction.CustomerID.ToString());
        _ = Task.Run(() =>
        {
            SendEmailToCustomersAsync(transaction, customer);
        });

        return true;
    }

    private async Task SendEmailToCustomersAsync(Domain.Entity.Transaction transaction, ApplicationUser customer)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "OrderCanceled.html");
        using (StreamReader reader = new StreamReader(filePath))
        {
            string content = await reader.ReadToEndAsync();
            content = Regex.Replace(content, "###ORDER_ID###", transaction.Id.ToString());
            content = Regex.Replace(content, "###NUM_ITEM_PURCHASED###", transaction.Order.OrderDetails.Count().ToString("N0"));
            content = Regex.Replace(content, "###TOTAL_PAYMENT###", transaction.Order.OrderDetails.Sum(o => o.Price * o.Quantity).ToString("N0"));
            content = Regex.Replace(content, "###SHIPPING_ADDRESS###", transaction.Order.ShippingAddress);
            content = Regex.Replace(content, "###ESTIMATED_DELIVERY_DATE###", DateTime.Now.AddDays(3).ToShortDateString());
            var emailOptions = new SendEmailOptions
            {
                Subject = "Payment Bill",
                Body = content,
                ToEmail = customer.Email!,
                ToName = customer.FullName!,
            };

            await _emailService.SendEmailAsync(emailOptions);
        }
    }
}
