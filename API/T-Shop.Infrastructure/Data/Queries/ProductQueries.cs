﻿using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries
{
    public class ProductQueries : BaseQuery<Product>, IProductQueries
    {
        public ProductQueries(ApplicationContext dbContext) : base(dbContext)
        { }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await dbSet
                    .Include(p => p.ProductReviews)
                    .OrderByDescending(p => p.ProductReviews.Count())
                    .Include(p => p.Color)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Type)
                    .Include(p => p.Model)
                        .ThenInclude(m => m.Brand)
                    .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await dbSet
                    .Where(p => p.Id.Equals(id))
                    .Include(p => p.Color)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Type)
                    .Include(p => p.Model)
                        .ThenInclude(m => m.Brand)
                    .Include(p => p.ProductReviews)
                    .FirstOrDefaultAsync();
        }
    }
}
