using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IBrandRepository
    {
        Task<List<BrandResponseModel>> GetBrandsAsync();
    }
}
