using MediatR;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypeById;
public class GetTypeByIdQuery : IRequest<TypeResponseModel>
{
    public Guid Id { get; set; }
}
