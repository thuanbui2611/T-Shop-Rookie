using MediatR;
using T_Shop.Shared.DTOs.ModelProduct.RequestModel;
using T_Shop.Shared.DTOs.ModelProduct.ResponseModel;

namespace T_Shop.Application.Features.ModelProduct.Commands.CreateModelProductCommand;
public class CreateModelProductCommand : ModelCreationRequestModel, IRequest<ModelProductResponseModel>
{
}
