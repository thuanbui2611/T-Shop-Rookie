using MediatR;
using T_Shop.Shared.DTOs.User.RequestModels;

namespace T_Shop.Application.Features.User.Commands.DisableUser;
public class LockUserCommand : LockUserRequestModel, IRequest<bool>
{

}
