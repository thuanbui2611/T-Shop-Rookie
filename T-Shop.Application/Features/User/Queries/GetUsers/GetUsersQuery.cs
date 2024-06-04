using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.User.QueryModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Queries.GetUsers;
public class GetUsersQuery : IRequest<(List<UserResponseModel>, PaginationMetaData)>
{
    public UserQuery UserQuery { get; set; }
    public PaginationRequestModel Pagination { get; set; }
}
