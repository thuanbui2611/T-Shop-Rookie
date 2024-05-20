using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IOrderQueries
{
    Task<Order> GetOrderByUserIdAsync(Guid id);

}
