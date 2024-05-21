using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Type.Commands.CreateType;
using T_Shop.Application.Features.Type.Commands.DeleteType;
using T_Shop.Application.Features.Type.Commands.UpdateType;
using T_Shop.Application.Features.Type.Queries.GetTypeById;
using T_Shop.Application.Features.Type.Queries.GetTypes;
using T_Shop.Controllers;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Type.QueryModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.WebAPI.Controllers;
[Route("api/type")]
[ApiController]
public class TypeController : ApiControllerBase
{
    /// <summary>
    /// Acquire Types information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet]
    public async Task<ActionResult<List<TypeResponseModel>>> GetTypesAsync(
         [FromQuery] PaginationRequestModel pagination,
         [FromQuery] TypeQuery typeQuery)
    {
        var (types, paginationMetaData) = await Mediator.Send(new GetTypesQuery()
        {
            TypeQuery = typeQuery,
            Pagination = pagination,
        });
        Response.AddPaginationHeader(paginationMetaData);
        return Ok(types);
    }

    /// <summary>
    /// Acquire type information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TypeResponseModel>> GetTypeByIdAsync([FromRoute] Guid id)
    {

        var type = await Mediator.Send(new GetTypeByIdQuery()
        {
            Id = id
        });
        return Ok(type);
    }

    /// <summary>
    /// Create a type
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost]
    public async Task<ActionResult<TypeResponseModel>> CreateTypeAsync([FromBody] CreateTypeCommand command)
    {

        var type = await Mediator.Send(command);
        return Created($"type/{type.Id}", type);
    }
    /// <summary>
    /// Update a type
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<TypeResponseModel>> UpdateTypeAsync([FromRoute] Guid id, [FromBody] UpdateTypeCommand command)
    {
        command.Id = id;
        var type = await Mediator.Send(command);
        return Ok(type);
    }

    /// <summary>
    /// Delete a type
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="204">Successfully deleted item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTypeAsync([FromRoute] Guid id)
    {

        var result = await Mediator.Send(new DeleteTypeCommand()
        {
            Id = id
        });
        if (!result) return BadRequest("Delete failed!");
        return NoContent();
    }
}
