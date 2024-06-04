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

        if (user.Email.ToLower().Trim() != request.Email.ToLower().Trim())
        {
            //Validate email existed
            var isEmailExisted = await _userManager.Users.AnyAsync(u => u.Email.ToLower().Trim() == request.Email.ToLower().Trim());
            if (isEmailExisted) throw new ConflictException("Email has existed!");
        }
        if (user.UserName.ToLower().Trim() != request.Username.ToLower().Trim())
        {
            //Validate username existed
            var isEmailExisted = await _userManager.Users.AnyAsync(u => u.UserName.ToLower().Trim() == request.Username.ToLower().Trim());
            if (isEmailExisted) throw new ConflictException("Username has existed!");
        }

        user.FullName = request.FullName;
        user.UserName = request.Username;
        user.Email = request.Email;
        user.DateOfBirth = request.DateOfBirth;
        user.Gender = request.Gender;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;

        //UploadImage
        if (request.AvatarUpload != null)
        {
            var image = await _cloudinaryService.AddImageAsync(request.AvatarUpload);
            user.Avatar = image.PublicID;
        }


        var resultUpdate = await _userManager.UpdateAsync(user);
        if (!resultUpdate.Succeeded)
        {
            throw new Exception("Update User Failed");
        }

        var result = _mapper.Map<UserResponseModel>(user);
        return result;
    }
}
