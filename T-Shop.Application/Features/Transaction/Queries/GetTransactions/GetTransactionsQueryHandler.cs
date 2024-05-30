using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactions;
public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, (List<TransactionResponseModel>, PaginationMetaData)>
{
    private readonly IMapper _mapper;
    private readonly ITransactionQueries _transactionQueries;

    public GetTransactionsQueryHandler(IMapper mapper, ITransactionQueries transactionQueries)
    {
        _mapper = mapper;
        _transactionQueries = transactionQueries;
    }

    public async Task<(List<TransactionResponseModel>, PaginationMetaData)> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var (transactions, pagination) = await _transactionQueries.GetTransactionsAsync(request.Pagination);
        var result = _mapper.Map<List<TransactionResponseModel>>(transactions);
        return (result, pagination);
    }
}
