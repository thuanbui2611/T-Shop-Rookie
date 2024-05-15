using MediatR;
using T_Shop.Shared.DTOs.Brand.RequestModel;
using T_Shop.Shared.DTOs.Brand.ResponseModel;

namespace T_Shop.Application.Features.Brand.Command.CreateBrand;
public class CreateBrandCommand : BrandCreationRequestModel, IRequest<BrandResponseModel>
{
}
