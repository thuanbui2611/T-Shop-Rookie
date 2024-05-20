using MediatR;
using T_Shop.Shared.DTOs.Transaction;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactions;
public class GetTransactionsQuery : IRequest<List<TransactionResponseModel>>
{
}
