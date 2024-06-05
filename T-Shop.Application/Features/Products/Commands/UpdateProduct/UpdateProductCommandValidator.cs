using FluentValidation;

namespace T_Shop.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(up => up)
            .Must(HaveValidImageCount)
            .WithMessage("Maximum of 5 images allowed.");

    }

    private bool HaveValidImageCount(UpdateProductCommand productToUpload)
    {
        int imagesUploadCount = productToUpload.ImagesUpload?.Count ?? 0;
        int imagesListCount = productToUpload.ImagesList?.Count ?? 0;

        return (imagesUploadCount + imagesListCount) <= 5;
    }
}
