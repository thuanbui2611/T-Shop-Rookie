using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.User.Commands.DisableUser;
using T_Shop.Application.Features.User.Commands.UpdateUser;
using T_Shop.Application.Features.User.Queries.GetUsers;
using T_Shop.Controllers;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.User.QueryModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.WebAPI.Controllers;
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
    /// Update a user
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseModel>> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
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
    [HttpPatch("{id}")]
    public async Task<ActionResult<UserResponseModel>> LockOrUnlockUser([FromBody] LockUserCommand command)
    {
        var user = await Mediator.Send(command);
        return Ok(user);
    }

}