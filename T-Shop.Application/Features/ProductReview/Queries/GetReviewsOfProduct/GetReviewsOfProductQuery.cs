using MediatR;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetReviewsOfProduct;
public class GetReviewsOfProductQuery : IRequest<List<ProductReviewResponseModel>>
{
    public Guid ProductID { get; set; }
}
