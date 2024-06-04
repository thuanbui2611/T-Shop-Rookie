using T_Shop.Shared.DTOs.Cart.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Shared.ViewModels.CartPage;
public class CartVM
{
    public CartResponseModel Cart { get; set; }
    public UserResponseModel CurrentUser { get; set; }
}
