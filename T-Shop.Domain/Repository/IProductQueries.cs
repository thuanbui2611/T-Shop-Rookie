using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository
{
    public interface IProductQueries
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetByIdAsync(Guid id);
    }
}
