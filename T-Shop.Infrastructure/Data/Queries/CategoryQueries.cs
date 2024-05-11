using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries
{
    public class CategoryQueries : BaseQuery<Category>, ICategoryQueries
    {
        public CategoryQueries(ApplicationContext dbContext) : base(dbContext)
        { }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await dbSet
                    .OrderBy(c => c.Name)
                    .Include(c => c.Products)
                    .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await dbSet
                    .Where(c => c.Id.Equals(id))
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckCategoryExisted(Guid id)
        {
            return await dbSet
                    .AnyAsync(c => c.Id.Equals(id));
        }

        public async Task<bool> CheckCategoryExisted(string name)
        {
            return await dbSet
                    .AnyAsync(c => c.Name.ToLower().Equals(name.ToLower()));
        }
    }
}
