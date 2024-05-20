using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.Transaction.Queries.GetTransactions;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Transaction;

namespace T_Shop.WebAPI.Controllers;
[Route("api/transaction")]
[ApiController]
public class TransactionController : ApiControllerBase
{
    /// <summary>
    /// Acquire transactions information
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet]
    public async Task<ActionResult<List<TransactionResponseModel>>> GetTransactionsAsync()
    {
        var transactions = await Mediator.Send(new GetTransactionsQuery());
        return Ok(transactions);
    }

}
