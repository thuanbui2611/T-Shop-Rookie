using MediatR;
using T_Shop.Shared.DTOs.Brand.QueryModel;
using T_Shop.Shared.DTOs.Brand.ResponseModel;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Application.Features.Brand.Queries.GetBrands;
public class GetBrandsQuery : IRequest<(List<BrandResponseModel>, PaginationMetaData)>
{
    public BrandQuery? BrandQuery { get; set; }
    public PaginationRequestModel Pagination { get; set; } = new PaginationRequestModel();
}
