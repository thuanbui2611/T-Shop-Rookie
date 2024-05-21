using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class ProductReviewQueries : BaseQuery<ProductReview>, IProductReviewQueries
{
    public ProductReviewQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }

    public async Task<int> CountTotalReviewByProductId(Guid productID)
    {
        return await dbSet.CountAsync(x => x.ProductID.Equals(productID));
    }

    public async Task<List<ProductReview>> GetAllProductReviewByProductIdAsync(Guid productID)
    {
        return await dbSet
                .Include(x => x.ProductReviewImages)
                .Where(x => x.ProductID.Equals(productID))
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
    }

    public async Task<decimal> GetAverageRatingByProductId(Guid productID)
    {
        return (decimal)await dbSet
                .Where(x => x.ProductID.Equals(productID))
                .AverageAsync(x => x.Rating);
    }
}
