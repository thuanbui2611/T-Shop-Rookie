using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Interfaces;

public interface IUserRepository
{
    UserResponseModel GetCurrentUser(HttpContext httpContext);
    Task<UserResponseModel> UpdateUserAsync(UserUpdateRequestModel user);
}
