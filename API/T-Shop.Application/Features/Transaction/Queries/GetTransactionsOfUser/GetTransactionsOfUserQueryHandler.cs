using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactionsOfUser;
public class GetTransactionsOfUserQueryHandler : IRequestHandler<GetTransactionsOfUserQuery, (List<TransactionResponseModel>, PaginationMetaData)>
{
    private readonly IMapper _mapper;
    private readonly ITransactionQueries _transactionQueries;

    public GetTransactionsOfUserQueryHandler(IMapper mapper, ITransactionQueries transactionQueries)
    {
        _mapper = mapper;
        _transactionQueries = transactionQueries;
    }

    public async Task<(List<TransactionResponseModel>, PaginationMetaData)> Handle(GetTransactionsOfUserQuery request, CancellationToken cancellationToken)
    {
        var (transactions, pagination) = await _transactionQueries.GetTransactionsByUserIdAsync(request.Pagination, request.UserId);
        var result = _mapper.Map<List<TransactionResponseModel>>(transactions);
        return (result, pagination);
    }
}
