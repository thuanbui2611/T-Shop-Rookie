using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactions;
public class GetTransactionsQuery : IRequest<(List<TransactionResponseModel>, PaginationMetaData)>
{
    public PaginationRequestModel Pagination { get; set; }
}
