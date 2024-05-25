using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetProductsAsync(ProductRequestParam productRequestParams);
        Task<ProductResponseModel> GetProductByIdAsync(Guid productId);

    }
}
