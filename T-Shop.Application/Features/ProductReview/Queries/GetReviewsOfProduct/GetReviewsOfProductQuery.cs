using MediatR;
using T_Shop.Shared.DTOs.Pagination;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Application.Features.ProductReview.Queries.GetReviewsOfProduct;
public class GetReviewsOfProductQuery : IRequest<(List<ProductReviewResponseModel>, PaginationMetaData)>
{
    public Guid ProductID { get; set; }
    public PaginationRequestModel Pagination { get; set; }
}
