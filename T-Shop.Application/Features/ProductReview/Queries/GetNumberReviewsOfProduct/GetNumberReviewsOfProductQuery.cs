using MediatR;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetNumberReviewsOfProduct;
public class GetNumberReviewsOfProductQuery : IRequest<ProductReviewCountResponseModel>
{
    public Guid ProductID { get; set; }
}
