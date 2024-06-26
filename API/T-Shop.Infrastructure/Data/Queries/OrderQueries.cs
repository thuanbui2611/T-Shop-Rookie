﻿using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class OrderQueries : BaseQuery<Order>, IOrderQueries
{
    public OrderQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }

    public async Task<Order?> GetOrderNotPaymentByUserIdAsync(Guid userID, bool trackChanges)
    {
        var query = trackChanges ? dbSet.AsTracking() : dbSet;
        query = query
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Color)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.ProductImages)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Type)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Model).ThenInclude(m => m.Brand);

        return await query.FirstOrDefaultAsync(o => o.UserID.Equals(userID) && !o.IsPayment);
    }

    public async Task<Order?> GetOrderByPaymentIntentIdAsync(string paymentIntentId, bool trackChanges)
    {
        var query = trackChanges ? dbSet.AsTracking() : dbSet;

        query = query
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Color)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.ProductImages)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Type)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Model)
                        .ThenInclude(m => m.Brand);

        return await query.FirstOrDefaultAsync(o => o.PaymentIntentID.Equals(paymentIntentId));
    }

    public async Task<Guid> GetOrderIdByUserId(Guid userId)
    {
        return await dbSet
            .Where(o => o.UserID.Equals(userId))
            .Select(o => o.Id)
            .FirstOrDefaultAsync();
    }
}
