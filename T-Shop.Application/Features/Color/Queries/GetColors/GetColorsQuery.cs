using MediatR;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Queries.GetColors;
public class GetColorsQuery : IRequest<List<ColorResponseModel>>
{
}
