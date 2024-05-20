using Microsoft.AspNetCore.Identity;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Infrastructure.SharedServices.Authentication;
public interface IAccountManager
{
    Task<IdentityResult> RegisterUser(UserCreationResquestModel userForRegistration);
    Task<bool> ValidateUser(UserAuthenRequestModel userForAuth);
    Task<string> CreateToken();

}
