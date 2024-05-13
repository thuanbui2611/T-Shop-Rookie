using MediatR;
using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.Authentication.Commands.Login;
public class LoginCommand : UserAuthenRequestModel, IRequest<UserAuthenResponseModel>
{
}
