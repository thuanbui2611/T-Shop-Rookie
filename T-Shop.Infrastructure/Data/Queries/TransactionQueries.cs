using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class TransactionQueries : BaseQuery<Transaction>, ITransactionQueries
{
    public TransactionQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }
    public async Task<Transaction> GetTransactionByPaymentIntentId(string paymentIntentID)
    {
        return await dbSet
            .Include(t => t.Order)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Color)
            .Include(t => t.Order)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Type)
            .Include(t => t.Order)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Model)
                .ThenInclude(m => m.Brand)
            .FirstOrDefaultAsync(t => t.Order.PaymentIntentID.Equals(paymentIntentID));
    }

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        return await dbSet
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Color)
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Type)
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Model)
               .ThenInclude(m => m.Brand)
            .ToListAsync();
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(Guid userID)
    {
        return await dbSet
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Color)
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Type)
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.Model)
               .ThenInclude(m => m.Brand)
            .Where(t => t.CustomerID.Equals(userID))
            .ToListAsync();
    }
}
