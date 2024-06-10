using MediatR;
using T_Shop.Shared.DTOs.ModelProduct.RequestModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Commands.UpdateModelProduct;
public class UpdateModelProductCommand : ModelUpdateRequestModel, IRequest<ModelProductResponseModel>
{

}
