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
           .OrderBy(x => x.Name)
           .ToListAsync();
    }

    public async Task<Model> GetModelByIdAsync(Guid id)
    {
        return await dbSet
           .Where(t => t.Id.Equals(id))
           .FirstOrDefaultAsync();
    }


}
