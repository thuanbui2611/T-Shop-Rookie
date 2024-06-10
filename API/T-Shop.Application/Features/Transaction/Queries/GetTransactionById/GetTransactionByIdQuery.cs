using MediatR;
using T_Shop.Shared.DTOs.Transaction.ResponseModel;

namespace T_Shop.Application.Features.Transaction.Queries.GetTransactionById;
public class GetTransactionByIdQuery : IRequest<TransactionWithCustomerResponseModel>
{
    public Guid TransactionId { get; set; }
}
