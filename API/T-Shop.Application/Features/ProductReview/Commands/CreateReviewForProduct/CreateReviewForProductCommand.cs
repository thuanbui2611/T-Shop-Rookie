using MediatR;
using T_Shop.Shared.DTOs.ProductReview.RequestModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Commands.CreateReviewForProduct;
public class CreateReviewForProductCommand : ProductReviewCreationRequestModel, IRequest<ProductReviewResponseModel>
{

}
