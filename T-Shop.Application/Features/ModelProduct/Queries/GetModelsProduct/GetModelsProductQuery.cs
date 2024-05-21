using MediatR;
using T_Shop.Shared.DTOs.ModelProduct.QueryModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Application.Features.ModelProduct.Queries.GetModelProducts;
public class GetModelsProductQuery : IRequest<(List<ModelProductResponseModel>, PaginationMetaData)>
{
    public ModelQuery ModelQuery { get; set; }
    public PaginationRequestModel Pagination { get; set; }
}
