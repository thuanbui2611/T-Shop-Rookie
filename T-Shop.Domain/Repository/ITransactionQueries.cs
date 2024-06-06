using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Pagination;
namespace T_Shop.Domain.Repository;
public interface ITransactionQueries
{
    Task<(List<Transaction>, PaginationMetaData)> GetTransactionsAsync(PaginationRequestModel pagination);
    Task<(List<Transaction>, PaginationMetaData)> GetTransactionsByUserIdAsync(PaginationRequestModel pagination, Guid userID);
    Task<Transaction?> GetTransactionByPaymentIntentId(string paymentIntentID);
    Task<Transaction?> GetTransactionByIdAsync(Guid transactionID, bool trackChanges);
}
