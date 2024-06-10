using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactionsOfUser;
public class GetTransactionsOfUserQuery : IRequest<(List<TransactionResponseModel>, PaginationMetaData)>
{
    public Guid UserId { get; set; }
    public PaginationRequestModel Pagination { get; set; }
}
