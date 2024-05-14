using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IModelQueries
{
    Task<List<Model>> GetModelsAsync();
    Task<Model> GetModelByIdAsync(Guid id);
}
