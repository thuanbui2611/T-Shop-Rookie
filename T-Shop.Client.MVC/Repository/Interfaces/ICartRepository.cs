using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Interfaces
{
    public interface ICartRepository
    {
        CartResponseModel GetCart();
        Task<CartResponseModel> GetCartByUserIdAsync(Guid userId);
        Task<CartResponseModel> AddToCartAsync(CartRequestModel cartRequestModel);
    }
}
