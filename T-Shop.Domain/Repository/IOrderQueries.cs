using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IOrderQueries
{
    Task<Order?> GetOrderNotPaymentByUserIdAsync(Guid userId, bool trackChanges);
    Task<Order?> GetOrderByPaymentIntentIdAsync(string paymentIntentId, bool trackChanges);
    Task<Guid> GetOrderIdByUserId(Guid userId);

}
