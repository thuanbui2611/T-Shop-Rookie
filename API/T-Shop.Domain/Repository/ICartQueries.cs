using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface ICartQueries
{
    Task<Cart> GetCartByIdAsync(Guid id);
    Task<Cart> GetCartByUserIdAsync(Guid userID);
    Task<Guid> GetCartIdByUserIdAsync(Guid userID);
    Task<bool> CheckIfItemExistedInCart(Guid cartID, Guid productID);
}
