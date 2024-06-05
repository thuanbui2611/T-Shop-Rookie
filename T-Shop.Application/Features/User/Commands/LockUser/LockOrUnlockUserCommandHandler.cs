using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using T_Shop.Application.Features.User.Commands.DisableUser;
using T_Shop.Domain.Exceptions;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Application.Features.User.Commands.LockUser;
public class LockOrUnlockUserCommandHandler : IRequestHandler<LockOrUnlockUserCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public LockOrUnlockUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<bool> Handle(LockOrUnlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserID.ToString());
        if (user == null) throw new NotFoundException("User is not found!");

        user.IsLocked = !user.IsLocked;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}
