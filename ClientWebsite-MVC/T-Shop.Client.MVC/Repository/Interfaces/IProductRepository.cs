using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.ViewModels.ProductsPage;

namespace T_Shop.Client.MVC.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductListVM> GetProductsAsync(ProductRequestParam productRequestParams);
        Task<ProductResponseModel> GetProductByIdAsync(Guid productId);
        Task<ProductReviewListVM> GetProductReviewsByIdAsync(ProductReviewRequestParam productReviewRequestParams, Guid productId);
    }
}
