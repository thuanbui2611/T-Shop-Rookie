﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.ModelProduct.Commands.CreateModelProductCommand;
using T_Shop.Application.Features.ModelProduct.Commands.DeleteModelProduct;
using T_Shop.Application.Features.ModelProduct.Commands.UpdateModelProduct;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelPagination;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelProductById;
using T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
using T_Shop.Controllers;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.ModelProduct.QueryModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.WebAPI.Controllers;

public class ModelController : ApiControllerBase
{
    /// <summary>
    /// Acquire models information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet]
    public async Task<ActionResult<List<ModelProductResponseModel>>> GetModelsAsync()
    {
        var models = await Mediator.Send(new GetModelsQuery());
        return Ok(models);
    }
    /// <summary>
    /// Acquire models information with pagination 
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("list")]
    public async Task<ActionResult<List<ModelProductResponseModel>>> GetModelsPaginationAsync(
        [FromQuery] PaginationRequestModel pagination,
        [FromQuery] ModelQuery modelQuery)
    {
        var (models, paginationMetaData) = await Mediator.Send(new GetModelsPaginationQuery()
        {
            ModelQuery = modelQuery,
            Pagination = pagination,
        });

        Response.AddPaginationHeader(paginationMetaData);
        return Ok(models);
    }

    /// <summary>
    /// Acquire model information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ModelProductResponseModel>> GetModelByIdAsync([FromRoute] Guid id)
    {
        var model = await Mediator.Send(new GetModelByIdQuery()
        {
            ID = id
        });
        return Ok(model);
    }

    /// <summary>
    /// Create a model
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ModelProductResponseModel>> CreateModelAsync([FromBody] CreateModelProductCommand command)
    {
        var model = await Mediator.Send(command);
        return Created($"model/{model.ID}", model);
    }
    /// <summary>
    /// Update a model
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ModelProductResponseModel>> UpdateModelAsync([FromRoute] Guid id, [FromBody] UpdateModelProductCommand command)
    {
        command.Id = id;
        var model = await Mediator.Send(command);
        return Ok(model);
    }

    /// <summary>
    /// Delete a model
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="204">Successfully deleted item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModelAsync([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteModelProductCommand()
        {
            ID = id
        });
        if (!result) return BadRequest("Delete failed!");
        return NoContent();
    }
}
