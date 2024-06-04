using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Queries.GetUserById;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseModel>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserByIdQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserResponseModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.ID.ToString());
        var result = _mapper.Map<UserResponseModel>(user);
        return result;
    }
}
