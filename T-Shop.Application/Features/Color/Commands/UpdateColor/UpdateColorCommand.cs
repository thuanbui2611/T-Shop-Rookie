using MediatR;
using T_Shop.Shared.DTOs.Color.RequestModel;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Commands.UpdateColor;
public class UpdateColorCommand : ColorUpdateRequestModel, IRequest<ColorResponseModel>
{
}
