using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class CartQueries : BaseQuery<Cart>, ICartQueries
{
    public CartQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }
    public async Task<Cart> GetCartByIdAsync(Guid id)
    {
        return await dbSet
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Color)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Model).ThenInclude(m => m.Brand)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Type)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.ProductImages)
            .Where(c => c.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task<Cart> GetCartByUserIdAsync(Guid userID)
    {
        return await dbSet
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Color)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Model).ThenInclude(m => m.Brand)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Type)
            .Include(c => c.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.ProductImages)
            .Where(c => c.UserID.Equals(userID))
            .FirstOrDefaultAsync();
    }
    public async Task<Guid> GetCartIdByUserIdAsync(Guid userID)
    {
        return await dbSet
            .Where(c => c.UserID.Equals(userID))
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIfItemExistedInCart(Guid cartID, Guid productID)
    {
        return await dbSet
            .Include(c => c.CartItems)
            .Where(c => c.Id.Equals(cartID))
            .AnyAsync(c => c.CartItems.Any(ci => ci.ProductID.Equals(productID)));
    }
}
