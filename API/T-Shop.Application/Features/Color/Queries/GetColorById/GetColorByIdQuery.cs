using MediatR;
using T_Shop.Shared.DTOs.Color.ResponseModel;

namespace T_Shop.Application.Features.Color.Queries.GetColorById;
public class GetColorByIdQuery : IRequest<ColorResponseModel>
{
    public Guid ID { get; set; }
}
