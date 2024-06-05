using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Client.MVC.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<(UserAuthenResponseModel, UserResponseModel)> Login(UserAuthenRequestModel user);
        Task<bool> Register(UserCreationResquestModel user);
    }
}
