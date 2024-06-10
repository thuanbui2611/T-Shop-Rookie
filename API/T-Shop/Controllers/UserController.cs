using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.User.Commands.DisableUser;
using T_Shop.Application.Features.User.Commands.UpdateUser;
using T_Shop.Application.Features.User.Queries.GetUserById;
using T_Shop.Application.Features.User.Queries.GetUsers;
using T_Shop.Controllers;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Type.ResponseModel;
using T_Shop.Shared.DTOs.User.QueryModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ApiControllerBase
{
    /// <summary>
    /// Acquire users information with pagination
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("list")]
    public async Task<ActionResult<List<UserResponseModel>>> GetUsersPaginationAsync(
         [FromQuery] PaginationRequestModel pagination,
         [FromQuery] UserQuery userQuery)
    {
        var (users, paginationMetaData) = await Mediator.Send(new GetUsersQuery()
        {
            UserQuery = userQuery,
            Pagination = pagination,
        });
        Response.AddPaginationHeader(paginationMetaData);
        return Ok(users);
    }
    /// <summary>
    /// Acquire user information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TypeResponseModel>> GetUserByIdAsync([FromRoute] Guid id)
    {

        var user = await Mediator.Send(new GetUserByIdQuery()
        {
            ID = id
        });
        return Ok(user);
    }

    /// <summary>
    /// Lock or unlock a user
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("lockOrUnlock")]
    public async Task<IActionResult> LockOrUnlockProductAsync([FromBody] LockOrUnlockUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Update a user
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseModel>> UpdateUserAsync([FromRoute] Guid id, [FromForm] UpdateUserCommand command)
    {
        command.ID = id;
        var user = await Mediator.Send(command);
        return Ok(user);
    }

    /// <summary>
    /// Lock or unlock user
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<ActionResult<UserResponseModel>> LockOrUnlockUser([FromBody] LockOrUnlockUserCommand command)
    {
        var user = await Mediator.Send(command);
        return Ok(user);
    }

}