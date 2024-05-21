using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.ProductReview.Commands.CreateReviewForProduct;
using T_Shop.Application.Features.Transaction.Commands.UpdateStatusTransaction;
using T_Shop.Application.Features.Transaction.Queries.GetTransactions;
using T_Shop.Controllers;
using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

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

    /// <summary>
    /// Create a review for product
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="201">Successfully created item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost("review")]
    public async Task<ActionResult<ProductReviewResponseModel>> CreateReviewForProductAsync([FromForm] CreateReviewForProductCommand command)
    {
        var createdReview = await Mediator.Send(command);
        return Created($"transaction/{createdReview.ProductID}/{createdReview.UserID}", createdReview);
    }

    /// <summary>
    /// Update status for transaction
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{transactionID}")]
    public async Task<ActionResult<ProductResponseModel>> UpdateStatusTransaction([FromRoute] Guid transactionID, [FromBody] UpdateStatusTransactionCommand command)
    {
        command.ID = transactionID;
        var updatedTransaction = await Mediator.Send(command);
        return Ok(updatedTransaction);
    }

}
