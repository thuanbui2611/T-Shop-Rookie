using FluentValidation;

namespace T_Shop.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(10).WithMessage("Name must less than 10 characters!");
        }
    }
}
