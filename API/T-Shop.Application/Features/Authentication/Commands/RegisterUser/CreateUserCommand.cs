using MediatR;
using Microsoft.AspNetCore.Identity;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Application.Features.Authentication.Commands.RegisterUser;
public class CreateUserCommand : UserCreationResquestModel, IRequest<IdentityResult>
{

}
