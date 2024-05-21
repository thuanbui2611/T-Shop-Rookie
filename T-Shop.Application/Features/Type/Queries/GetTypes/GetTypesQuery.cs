using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.Type.QueryModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Queries.GetTypes;
public class GetTypesQuery : IRequest<(List<TypeResponseModel>, PaginationMetaData)>
{
    public TypeQuery TypeQuery { get; set; }
    public PaginationRequestModel Pagination { get; set; }
}
