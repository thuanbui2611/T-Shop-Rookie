using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Application.Features.Brand.Command.CreateBrand;
using T_Shop.Application.Features.Brand.Command.DeleteBrand;
using T_Shop.Application.Features.Brand.Command.UpdateBrand;
using T_Shop.Application.Features.Brand.Queries.GetBrandById;
using T_Shop.Application.Features.Brand.Queries.GetBrands;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.WebAPI.Controllers;


public class BrandController : ApiControllerBase
{
    /// <summary>
    /// Acquire brands information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet]
    public async Task<ActionResult<List<BrandResponseModel>>> GetBrandsAsync()
    {
        var brands = await Mediator.Send(new GetBrandsQuery());
        return Ok(brands);
    }

    /// <summary>
    /// Acquire type information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<BrandResponseModel>> GetBrandByIdAsync([FromRoute] Guid id)
    {
        var brand = await Mediator.Send(new GetBrandByIdQuery()
        {
            ID = id
        });
        return Ok(brand);
    }

    /// <summary>
    /// Create a brand
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost]
    public async Task<ActionResult<BrandResponseModel>> CreateBrandAsync([FromBody] CreateBrandCommand command)
    {
        var brand = await Mediator.Send(command);
        return Created($"brand/{brand.ID}", brand);
    }
    /// <summary>
    /// Update a brand
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<BrandResponseModel>> UpdateBrandAsync([FromRoute] Guid id, [FromBody] UpdateBrandCommand command)
    {
        if (id != command.ID)
        {
            throw new BadRequestException("Brand ID do not match");
        }
        var brand = await Mediator.Send(command);
        return Ok(brand);
    }

    /// <summary>
    /// Delete a brand
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="204">Successfully deleted item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteBrandCommand()
        {
            ID = id
        });
        if (!result) return BadRequest("Delete failed!");
        return NoContent();
    }
}
