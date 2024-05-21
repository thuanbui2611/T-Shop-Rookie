using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using T_Shop.Application.Common.Constants;
using T_Shop.Domain.Exceptions;
using T_Shop.Domain.Repository;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Commands.UpdateStatusTransaction;
public class UpdateStatusTransactionCommandHandler : IRequestHandler<UpdateStatusTransactionCommand, TransactionResponseModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionQueries _transactionQueries;

    private readonly IGenericRepository<Domain.Entity.Transaction> _transactionRepository;

    public UpdateStatusTransactionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ITransactionQueries transactionQueries)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _transactionRepository = unitOfWork.GetBaseRepo<Domain.Entity.Transaction>();
        _transactionQueries = transactionQueries;
    }

    public async Task<TransactionResponseModel> Handle(UpdateStatusTransactionCommand request, CancellationToken cancellationToken)
    {
        //Validate status updated
        var isValid = TransactionStatusConstants.AVAILABLE_UPDATE_TRANSACTION_STATUS.Contains(request.Status);
        if (!isValid) throw new BadRequestException($"Only allow update to status: " +
            $"{String.Join(", ", TransactionStatusConstants.AVAILABLE_UPDATE_TRANSACTION_STATUS)}");

        //Validate request
        var transaction = await _transactionQueries.GetTransactionsByIdAsync(request.ID);

        if (transaction == null) throw new BadRequestException("Transaction not found");

        transaction.Status = request.Status;
        if (!request.Reason.IsNullOrEmpty())
        {
            transaction.Reason = request.Reason;
        }
        _transactionRepository.Update(transaction);
        await _unitOfWork.CompleteAsync();

        var result = _mapper.Map<TransactionResponseModel>(transaction);
        return result;
    }
}
