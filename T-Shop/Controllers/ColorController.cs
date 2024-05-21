using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Color.Commands.CreateColor;
using T_Shop.Application.Features.Color.Commands.DeleteColor;
using T_Shop.Application.Features.Color.Commands.UpdateColor;
using T_Shop.Application.Features.Color.Queries.GetColorById;
using T_Shop.Application.Features.Color.Queries.GetColors;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.WebAPI.Controllers;

public class ColorController : ApiControllerBase
{
    /// <summary>
    /// Acquire Colors information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet]
    public async Task<ActionResult<List<ColorResponseModel>>> GetColorsAsync()
    {

        var colors = await Mediator.Send(new GetColorsQuery());
        return Ok(colors);
    }

    /// <summary>
    /// Acquire color information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ColorResponseModel>> GetColorByIdAsync([FromRoute] Guid id)
    {

        var color = await Mediator.Send(new GetColorByIdQuery()
        {
            ID = id
        });
        return Ok(color);
    }

    /// <summary>
    /// Create a color
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost]
    public async Task<ActionResult<ColorResponseModel>> CreateColorAsync([FromBody] CreateColorCommand command)
    {

        var color = await Mediator.Send(command);
        return Created($"color/{color.ID}", color);
    }
    /// <summary>
    /// Update a color
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<ColorResponseModel>> UpdateColorAsync([FromRoute] Guid id, [FromBody] UpdateColorCommand command)
    {
        command.ID = id;
        var color = await Mediator.Send(command);
        return Ok(color);
    }

    /// <summary>
    /// Delete a color
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="204">Successfully deleted item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteColorAsync([FromRoute] Guid id)
    {

        var result = await Mediator.Send(new ColorDeleteCommand()
        {
            ID = id
        });
        if (!result) return BadRequest("Delete failed!");
        return NoContent();
    }
}
