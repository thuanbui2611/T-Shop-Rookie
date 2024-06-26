﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Cart.Commands.AddItemToCartQuery;
using T_Shop.Application.Features.Cart.Commands.DeleteIItemCart;
using T_Shop.Application.Features.Cart.Commands.UpdateCart;
using T_Shop.Application.Features.Cart.Queries.GetCartByUserId;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.WebAPI.Controllers;
[Authorize]
[Route("api/cart")]
[ApiController]
public class CartController : ApiControllerBase
{
    /// <summary>
    /// Acquire cart information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{userID}")]
    public async Task<ActionResult<CartResponseModel>> GetCartOfUserAsync([FromRoute] Guid userID)
    {
        var cart = await Mediator.Send(new GetCartByUserIdQuery()
        {
            UserID = userID
        });
        return Ok(cart);
    }


    /// <summary>
    /// Add item to cart
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost]
    public async Task<ActionResult<CartResponseModel>> AddItemToCartAsync([FromBody] AddItemToCartCommand command)
    {
        var cart = await Mediator.Send(command);
        return Created($"cart/{cart.ID}", cart);
    }

    /// <summary>
    /// Update a brand
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{userID}")]
    public async Task<ActionResult<CartResponseModel>> UpdateCartAsync([FromRoute] Guid userID, [FromBody] UpdateCartCommand command)
    {
        command.UserID = userID;
        var cart = await Mediator.Send(command);
        return Created($"cart/{cart.ID}", cart);
    }

    /// <summary>
    /// Delete a item in cart
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="204">Successfully deleted item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpDelete("{userID}/{productID}")]
    public async Task<IActionResult> DeleteItemInCartAsync([FromRoute] Guid userID, [FromRoute] Guid productID)
    {

        await Mediator.Send(new DeleteItemCartCommand()
        {
            ProductID = productID,
            UserID = userID
        });
        return NoContent();

    }
}
