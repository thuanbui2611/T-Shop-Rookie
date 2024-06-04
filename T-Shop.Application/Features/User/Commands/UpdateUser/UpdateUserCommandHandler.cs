using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Exceptions;
using T_Shop.Infrastructure.Persistence.IdentityModels;
using T_Shop.Infrastructure.SharedServices.CloudinaryService;
using T_Shop.Shared.DTOs.User.ResponseModels;

namespace T_Shop.Application.Features.User.Commands.UpdateUser;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseModel>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICloudinaryService _cloudinaryService;
    public UpdateUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<UserResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.ID.ToString());

        if (user == null) throw new NotFoundException("User is not found!");

        if (user.Email!.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
        {
            //Validate email existed
            var isEmailExisted = await _userManager.Users.AnyAsync(u => u.Email!.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
            if (isEmailExisted) throw new ConflictException("Email has existed!");
        }
        if (user.UserName!.Equals(request.Username, StringComparison.OrdinalIgnoreCase))
        {
            //Validate username existed
            var isEmailExisted = await _userManager.Users.AnyAsync(u => u.UserName!.Equals(request.Username, StringComparison.OrdinalIgnoreCase));
            if (isEmailExisted) throw new ConflictException("Username has existed!");
        }

        var userToUpdate = new ApplicationUser()
        {
            FullName = request.FullName,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            Gender = request.Gender,
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
        };

        //UploadImage
        if (request.AvatarUpload != null)
        {
            var image = await _cloudinaryService.AddImageAsync(request.AvatarUpload);
            userToUpdate.Avatar = image.PublicID;
        }

        var updateUser = await _userManager.UpdateAsync(userToUpdate);
        if (!updateUser.Succeeded)
        {
            throw new Exception("Update User Failed");
        }

        var result = _mapper.Map<UserResponseModel>(userToUpdate);
        return result;
    }
}
