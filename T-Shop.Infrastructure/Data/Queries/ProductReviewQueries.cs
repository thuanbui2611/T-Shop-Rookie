using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;
using T_Shop.Shared.DTOs.Pagination;

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

    public async Task<(List<ProductReview>, PaginationMetaData)> GetProductReviewsByProductIdAsync(Guid productID, PaginationRequestModel pagination)
    {
        var totalItemCount = await dbSet
            .Where(pr => pr.ProductID.Equals(productID))
            .CountAsync();

        var reviewsOfProduct = await dbSet
                .Include(x => x.ProductReviewImages)
                .Where(x => x.ProductID.Equals(productID))
                .OrderByDescending(x => x.CreatedAt)
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .ToListAsync();
        var paginationMetaData = new PaginationMetaData(totalItemCount, pagination.pageSize, pagination.pageNumber);
        return (reviewsOfProduct, paginationMetaData);
    }

    public async Task<decimal> GetAverageRatingByProductId(Guid productID)
    {
        return (decimal)await dbSet
                .Where(x => x.ProductID.Equals(productID))
                .AverageAsync(x => x.Rating);
    }
}
