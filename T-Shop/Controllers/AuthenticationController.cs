using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Authentication.Commands.Login;
using T_Shop.Application.Features.Authentication.Commands.RegisterUser;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.WebAPI.Controllers;
[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ApiControllerBase
{
    /// <summary>
    /// Sign up and create a user for the system.
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully create a user.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Created();
    }


    /// <summary>
    /// Login and create create a JWT token for the system.
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully create a JWT token.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost("Login")]
    public async Task<ActionResult<UserAuthenResponseModel>> Login([FromBody] LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
