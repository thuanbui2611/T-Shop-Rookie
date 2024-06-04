using MediatR;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Queries.GetUserById;
public class GetUserByIdQuery : IRequest<UserResponseModel>
{
    public Guid ID { get; set; }
}
