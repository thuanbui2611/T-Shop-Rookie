using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IBrandService
    {
        Task<BrandResponseModel> GetBrandsAsync();
    }
}
