using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IBrandQueries
{
    Task<List<Brand>> GetBrandsAsync();
    Task<Brand> GetBrandByIdAsync(Guid id);
    Task<bool> CheckIsBrandExisted(string name);
}
