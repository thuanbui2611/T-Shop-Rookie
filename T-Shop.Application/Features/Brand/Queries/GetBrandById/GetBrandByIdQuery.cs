using MediatR;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Queries.GetBrandById;
public class GetBrandByIdQuery : IRequest<BrandResponseModel>
{
    public Guid ID { get; set; }
}
