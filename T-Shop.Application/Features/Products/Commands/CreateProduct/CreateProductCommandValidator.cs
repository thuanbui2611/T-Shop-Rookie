using FluentValidation;

namespace T_Shop.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.ImagesUpload)
            .NotEmpty()
            .Must(files => files.Count <= 5)
            .WithMessage("Maximum of 5 images allowed.")
            ;

    }

}
