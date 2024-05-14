using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Authentication.Commands.Login;
using T_Shop.Application.Features.Authentication.Commands.RegisterUser;
using T_Shop.Controllers;

namespace T_Shop.WebAPI.Controllers;
[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ApiControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Created();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
