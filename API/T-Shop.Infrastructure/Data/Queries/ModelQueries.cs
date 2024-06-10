using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class ModelQueries : BaseQuery<Model>, IModelQueries
{
    public ModelQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Model>> GetModelsAsync()
    {
        return await dbSet
           .Include(m => m.Brand)
           .OrderBy(m => m.Name)
           .ToListAsync();
    }

    public async Task<Model> GetModelByIdAsync(Guid id)
    {
        return await dbSet
           .Include(m => m.Brand)
           .Where(m => m.Id.Equals(id))
           .FirstOrDefaultAsync();
    }

    public async Task<List<Model>> GetModelsByNameAsync(string name)
    {
        return await dbSet
            .Where(m => m.Name.ToLower().Trim().Equals(name.ToLower().Trim()))
            .ToListAsync();
    }

    public async Task<bool> CheckIsModelExisted(string name)
    {
        return await dbSet
            .AnyAsync(t => t.Name.ToLower().Equals(name.ToLower()));
    }
}
