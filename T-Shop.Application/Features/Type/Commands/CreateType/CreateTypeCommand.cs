using MediatR;
using T_Shop.Shared.DTOs.Type.RequestModel;
using T_Shop.Shared.DTOs.Type.ResponseModel;

namespace T_Shop.Application.Features.Type.Commands.CreateType;
public class CreateTypeCommand : TypeCreationRequestModel, IRequest<TypeResponseModel>
{
}
