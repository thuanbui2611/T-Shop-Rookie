using MediatR;
using T_Shop.Shared.DTOs.Brand.RequestModel;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Command.UpdateBrand;
public class UpdateBrandCommand : BrandUpdateRequestModel, IRequest<BrandResponseModel>
{
}
