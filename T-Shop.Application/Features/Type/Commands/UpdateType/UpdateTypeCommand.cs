using MediatR;
using T_Shop.Shared.DTOs.Type.RequestModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Commands.UpdateType;
public class UpdateTypeCommand : TypeUpdateRequestModel, IRequest<TypeResponseModel>
{
}
