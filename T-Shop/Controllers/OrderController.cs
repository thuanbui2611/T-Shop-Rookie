using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using T_Shop.Application.Features.Order.Commands;
using T_Shop.Application.Features.Transaction.Commands.CreateTransactionFromStripeEvent;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Cart.ResponseModel;

namespace T_Shop.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ApiControllerBase
{
    private readonly IConfiguration _configuration;
    public OrderController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
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

    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        try
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["StripeSettings:WhSecret"]);
            var transaction = await Mediator.Send(new CreateTransactionFromStripeEventCommand()
            {
                stripeEvent = stripeEvent
            });
            return new EmptyResult();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
