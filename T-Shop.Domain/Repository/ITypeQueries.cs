using TypeProduct = T_Shop.Domain.Entity.TypeProduct;

namespace T_Shop.Domain.Repository;
public interface ITypeQueries
{
    Task<List<TypeProduct>> GetTypesAsync();
    Task<TypeProduct> GetTypeByIdAsync(Guid id);
    Task<bool> CheckIsTypeExisted(string name);
}
