using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class ModelQueries : BaseQuery<Model>, IModelQueries
{
    public ModelQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }
    public async Task<Model> GetModelByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Model>> GetModelsAsync()
    {
        throw new NotImplementedException();
    }
}
