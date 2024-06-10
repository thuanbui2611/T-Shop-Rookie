using MediatR;

namespace T_Shop.Application.Features.User.Commands.DisableUser;
public class LockOrUnlockUserCommand : IRequest<bool>
{
    public Guid UserID { get; set; }
}
