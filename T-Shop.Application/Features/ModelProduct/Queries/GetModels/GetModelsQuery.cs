using MediatR;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
public class GetModelsQuery : IRequest<List<ModelProductResponseModel>>
{
}
