using T_Shop.Shared.DTOs.Cart.RequestModel;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.Client.MVC.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartResponseModel>> GetCartByUserIdAsync(Guid userId);
        Task<CartResponseModel> AddToCartAsync(CartRequestModel cartRequestModel);
    }
}
