using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface ITransactionQueries
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task<List<Transaction>> GetTransactionsByUserIdAsync(Guid userID);
    Task<Transaction> GetTransactionByPaymentIntentId(string paymentIntentID);
    Task<Transaction> GetTransactionsByIdAsync(Guid transactionID);
}
