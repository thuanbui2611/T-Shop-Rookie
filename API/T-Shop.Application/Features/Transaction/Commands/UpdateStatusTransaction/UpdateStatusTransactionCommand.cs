using MediatR;
using T_Shop.Shared.DTOs.Transaction.RequestModel;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Commands.UpdateStatusTransaction;
public class UpdateStatusTransactionCommand : TransactionUpdateRequestModel, IRequest<TransactionResponseModel>
{

}
