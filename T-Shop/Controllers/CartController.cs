using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Common.Exceptions;
using T_Shop.Application.Features.Cart.Commands.AddItemToCartQuery;
using T_Shop.Application.Features.Cart.Commands.UpdateCart;
using T_Shop.Application.Features.Cart.Queries.GetCartByUserId;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.WebAPI.Controllers;
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
    [HttpPut("{id}")]
    public async Task<ActionResult<CartResponseModel>> UpdateCartAsync([FromRoute] Guid id, [FromBody] UpdateCartCommand command)
    {
        if (id != command.UserID)
        {
            throw new BadRequestException("User ID do not match");
        }
        var cart = await Mediator.Send(command);
        return Created($"cart/{cart.ID}", cart);
    }
}
