using MediatR;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Queries.GetBrands;
public class GetBrandsQuery : IRequest<List<BrandResponseModel>>
{
}
