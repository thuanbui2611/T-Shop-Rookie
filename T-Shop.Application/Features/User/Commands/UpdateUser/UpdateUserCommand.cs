using MediatR;
using T_Shop.Shared.DTOs.User.RequestModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Commands.UpdateUser;
public class UpdateUserCommand : UserUpdateRequestModel, IRequest<UserResponseModel>
{
}
