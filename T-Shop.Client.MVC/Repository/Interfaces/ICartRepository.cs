using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<CartResponseModel> GetCurrentCart(Guid userId);
        Task<CartResponseModel> GetCartByUserIdAsync(Guid userId);
        Task<CartResponseModel> AddToCartAsync(CartRequestModel cartRequestModel);
        Task<CartResponseModel> UpdateCartItemAsync(CartRequestModel cartRequestModel);
        Task<bool> DeleteCartItemAsync(CartItemDeleteRequestModel cartItemRequestModel);
        void ClearCart();
    }
}
