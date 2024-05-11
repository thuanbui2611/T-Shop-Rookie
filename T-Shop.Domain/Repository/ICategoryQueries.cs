using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository
{
    public interface ICategoryQueries
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<bool> CheckCategoryExisted(Guid id);
        Task<bool> CheckCategoryExisted(string name);
    }
}
