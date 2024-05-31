using T_Shop.Shared.DTOs.Transaction.RequestModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;
using T_Shop.Shared.ViewModels.TransactionPage;

namespace T_Shop.Client.MVC.Repository.Interfaces;

public interface ITransactionRepository
{
    Task<TransactionVM> GetTransactionsOfUserAsync(TransactionRequestParam transactionRequestParam, Guid userId);
    Task<TransactionResponseModel> GetTransactionByIdAsync(Guid transactionId);
    Task<TransactionResponseModel> UpdateTransactionStatusAsync(TransactionUpdateRequestModel transaction);
}
