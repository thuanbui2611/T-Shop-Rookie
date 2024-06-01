using T_Shop.Domain.Entity;
using T_Shop.Shared.DTOs.Pagination;

namespace T_Shop.Domain.Repository;
public interface IProductReviewQueries
{
    Task<int> CountTotalReviewByProductId(Guid productID);
    Task<decimal> GetAverageRatingByProductId(Guid productID);
    Task<(List<ProductReview>, PaginationMetaData)> GetProductReviewsByProductIdAsync(Guid productID, PaginationRequestModel pagination);
}
