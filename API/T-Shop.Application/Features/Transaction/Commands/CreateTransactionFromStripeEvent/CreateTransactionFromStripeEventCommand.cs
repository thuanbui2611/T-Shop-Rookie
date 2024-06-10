using MediatR;
using Stripe;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Commands.CreateTransactionFromStripeEvent;
public record CreateTransactionFromStripeEventCommand : IRequest<TransactionResponseModel>
{
    public required Event stripeEvent { get; set; }
}
