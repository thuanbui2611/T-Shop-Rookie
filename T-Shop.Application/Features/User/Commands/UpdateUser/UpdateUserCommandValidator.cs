using FluentValidation;
using Microsoft.AspNetCore.Identity;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Application.Features.User.Commands.UpdateUser;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(u => u.Email)
            .NotEmpty();
        RuleFor(u => u.Address)
            .NotEmpty();
        RuleFor(u => u.PhoneNumber)
            .NotEmpty();
        RuleFor(u => u.Username)
            .MaximumLength(50)
            .NotEmpty();
    }

}
