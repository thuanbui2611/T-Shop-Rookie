using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IOrderQueries
{
    Task<Order> GetOrderNotPaymentByUserIdAsync(Guid userId);
    Task<Order> GetOrderByPaymentIntentIdAsync(string paymentIntentId);
    Task<Guid> GetOrderIdByUserId(Guid userId);

}
