using T_Shop.Domain.Entity;

namespace T_Shop.Domain.Repository;
public interface IProductReviewQueries
{
    Task<int> CountTotalReviewByProductId(Guid productID);
    Task<decimal> GetAverageRatingByProductId(Guid productID);
    Task<List<ProductReview>> GetAllProductReviewByProductIdAsync(Guid productID);
}
