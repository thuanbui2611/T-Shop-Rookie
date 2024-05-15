using MediatR;
using T_Shop.Shared.DTOs.Color.RequestModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.CreateColor;
public class CreateColorCommand : ColorCreationRequestModel, IRequest<ColorResponseModel>
{
}
