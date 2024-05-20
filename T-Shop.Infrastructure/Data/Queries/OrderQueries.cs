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
    public async Task<Order> GetOrderByUserIdAsync(Guid userID)
    {
        return await dbSet
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Color)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.ProductImages)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Type)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Model).ThenInclude(m => m.Brand)
            .FirstOrDefaultAsync(o => o.UserID.Equals(userID));

    }
}