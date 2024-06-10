using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Queries.GetUsers;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, (List<UserResponseModel>, PaginationMetaData)>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUsersQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<(List<UserResponseModel>, PaginationMetaData)> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
                    .OrderBy(u => u.FullName)
                    .Skip(request.Pagination.pageSize * (request.Pagination.pageNumber - 1))
                    .Take(request.Pagination.pageSize)
                    .ToListAsync();

        var totalUsersCount = await _userManager.Users.CountAsync();
        var paginationMetaData = new PaginationMetaData(totalUsersCount, request.Pagination.pageSize, request.Pagination.pageNumber);

        var usersResponse = _mapper.Map<List<UserResponseModel>>(users);

        return (usersResponse, paginationMetaData);
    }
}
