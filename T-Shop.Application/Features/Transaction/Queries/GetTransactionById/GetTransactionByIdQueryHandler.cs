using AutoMapper;
using MediatR;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactionById;
public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponseModel>
{
    private readonly IMapper _mapper;
    private readonly ITransactionQueries _transactionQueries;

    public GetTransactionByIdQueryHandler(IMapper mapper, ITransactionQueries transactionQueries)
    {
        _mapper = mapper;
        _transactionQueries = transactionQueries;
    }

    public async Task<TransactionResponseModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionQueries.GetTransactionsByIdAsync(request.TransactionId);
        if (transaction == null)
        {
            throw new BadRequestException(message: "Transaction not found");
        }
        var result = _mapper.Map<TransactionResponseModel>(transaction);
        return result;
    }
}
