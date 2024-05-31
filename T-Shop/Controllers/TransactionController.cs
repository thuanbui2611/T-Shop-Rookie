using Microsoft.AspNetCore.Mvc;
using T_Shop.Application.Features.ProductReview.Commands.CreateReviewForProduct;
using T_Shop.Application.Features.Transaction.Commands.UpdateStatusTransaction;
using T_Shop.Application.Features.Transaction.Queries.GetTransactionById;
using T_Shop.Application.Features.Transaction.Queries.GetTransactions;
using T_Shop.Application.Features.Transaction.Queries.GetTransactionsOfUser;
using T_Shop.Controllers;
using T_Shop.Extensions;
using T_Shop.Shared.DTOs.Pagination;
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
    public async Task<ActionResult<List<TransactionResponseModel>>> GetTransactionsAsync(
         [FromQuery] PaginationRequestModel pagination
        )
    {
        var (transactions, paginationMetaData) = await Mediator.Send(new GetTransactionsQuery()
        {
            Pagination = pagination
        }); ;
        Response.AddPaginationHeader(paginationMetaData);
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
    /// Acquire transaction information by identification
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionResponseModel>> GetTransactionByIdAsync([FromRoute] Guid id)
    {

        var transaction = await Mediator.Send(new GetTransactionByIdQuery()
        {
            TransactionId = id
        });
        return Ok(transaction);

    }

    /// <summary>
    /// Acquire transaction information of user
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully get items information.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<TransactionResponseModel>> GetTransactionByIdAsync(
        [FromRoute] Guid userId,
        [FromQuery] PaginationRequestModel pagination)
    {

        var (transactions, paginationMetaData) = await Mediator.Send(new GetTransactionsOfUserQuery()
        {
            UserId = userId,
            Pagination = pagination
        });
        Response.AddPaginationHeader(paginationMetaData);
        return Ok(transactions);
    }

    /// <summary>
    /// Update status for transaction
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully updated item.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPut("{transactionID}")]
    public async Task<ActionResult<TransactionResponseModel>> UpdateStatusTransaction([FromRoute] Guid transactionID, [FromBody] UpdateStatusTransactionCommand command)
    {
        command.ID = transactionID;
        var updatedTransaction = await Mediator.Send(command);
        return Ok(updatedTransaction);
    }

}
