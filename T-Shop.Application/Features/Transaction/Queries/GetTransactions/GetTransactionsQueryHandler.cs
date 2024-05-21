using AutoMapper;
using MediatR;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactions;
public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionResponseModel>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionQueries _transactionQueries;

    public GetTransactionsQueryHandler(IMapper mapper, ITransactionQueries transactionQueries)
    {
        _mapper = mapper;
        _transactionQueries = transactionQueries;
    }

    public async Task<List<TransactionResponseModel>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _transactionQueries.GetTransactionsAsync();
        var result = _mapper.Map<List<TransactionResponseModel>>(transactions);
        return result;
    }
}
