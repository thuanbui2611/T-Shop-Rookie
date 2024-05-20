using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Order.Commands;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ApiControllerBase
{
    /// <summary>
    /// Checkout order to create payment intent for client to pay
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost]
    public async Task<ActionResult<CartResponseModel>> CreateOrUpdateOrderAsync([FromBody] CreateOrUpdateOrderCommand command)
    {
        var order = await Mediator.Send(command);
        return Ok(order);
    }
}
