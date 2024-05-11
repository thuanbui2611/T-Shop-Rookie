using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries
{
    public class ProductQueries : BaseQuery<Product>, IProductQueries
    {
        public ProductQueries(ApplicationContext dbContext) : base(dbContext)
        { }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbSet
                    .OrderBy(p => p.Name)
                    .Include(p => p.Category)
                    .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await dbSet
                    .Where(p => p.Id.Equals(id))
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync();
        }


    }
}
