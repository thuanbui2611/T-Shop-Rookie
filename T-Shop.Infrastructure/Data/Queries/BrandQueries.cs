using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class BrandQueries : BaseQuery<Brand>, IBrandQueries
{
    public BrandQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }
    public async Task<List<Brand>> GetBrandsAsync()
    {
        return await dbSet
            .OrderBy(b => b.Name)
            .ToListAsync();
    }

    public async Task<Brand> GetBrandByIdAsync(Guid id)
    {
        return await dbSet
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
    }
}
