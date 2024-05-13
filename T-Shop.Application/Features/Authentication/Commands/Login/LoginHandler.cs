using MediatR;
using T_Shop.Application.Common.Interface;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.Authentication.Commands.Login;
public class LoginHandler : IRequestHandler<LoginCommand, UserAuthenResponseModel>
{
    private readonly IAccountManager _accountManager;

    public LoginHandler(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task<UserAuthenResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (!await _accountManager.ValidateUser(request)) throw new UnauthorizedAccessException("Wrong email or password");
        var token = await _accountManager.CreateToken();
        var result = new UserAuthenResponseModel()
        {
            Token = token
        };
        return result;
    }
}
