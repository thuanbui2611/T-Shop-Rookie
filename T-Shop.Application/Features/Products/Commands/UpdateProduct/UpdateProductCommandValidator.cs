using FluentValidation;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(up => up.ImagesUpload.Count + up.ImagesList.Count)
            .LessThanOrEqualTo(5)
            .WithMessage("Maximum of 5 images allowed.");

    }
}
