using MediatR;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProductById;
public class GetModelByIdQuery : IRequest<ModelProductResponseModel>
{
    public Guid ID { get; set; }
}
