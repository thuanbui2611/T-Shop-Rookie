using MediatR;
using Microsoft.AspNetCore.Identity;
using T_Shop.Application.Common.Interface;
using T_Shop.Application.Common.ServiceInterface;

namespace T_Shop.Application.Features.Authentication.Commands.RegisterUser;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly IAccountManager _accountManager;
    private readonly ICloudinaryService _cloudinaryService;


    public CreateUserHandler(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountManager.RegisterUser(request);
        return result;
    }
}
