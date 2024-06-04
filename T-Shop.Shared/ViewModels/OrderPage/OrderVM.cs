using T_Shop.Shared.DTOs.Order.ResponseModel;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Shared.ViewModels.OrderPage;
public class OrderVM
{
    public OrderResponseModel Order { get; set; }
    public string? StripePublishableKey { get; set; }
    public string? StripeSecretKey { get; set; }
    public UserResponseModel CurrentUser { get; set; }
}
