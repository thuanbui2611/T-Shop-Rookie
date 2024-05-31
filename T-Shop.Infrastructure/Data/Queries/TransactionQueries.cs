using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;
using T_Shop.Shared.DTOs.Pagination;

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

    public async Task<(List<Transaction>, PaginationMetaData)> GetTransactionsAsync(PaginationRequestModel pagination)
    {
        var totalItemCount = await dbSet.CountAsync();

        var transactions = await dbSet
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
            .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.ProductImages)
            .OrderByDescending(t => t.CreatedAt)
            .Skip(pagination.pageSize * (pagination.pageNumber - 1))
            .Take(pagination.pageSize)
            .ToListAsync();
        var paginationMetaData = new PaginationMetaData(totalItemCount, pagination.pageSize, pagination.pageNumber);
        return (transactions, paginationMetaData);
    }

    public async Task<(List<Transaction>, PaginationMetaData)> GetTransactionsByUserIdAsync(PaginationRequestModel pagination, Guid userID)
    {
        var totalItemCount = await dbSet
            .Where(t => t.CustomerID.Equals(userID))
            .CountAsync();
        var paginationMetaData = new PaginationMetaData(totalItemCount, pagination.pageSize, pagination.pageNumber);

        var transactionsOfUser = await dbSet
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
            .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.ProductImages)
            .Where(t => t.CustomerID.Equals(userID))
            .ToListAsync();

        return (transactionsOfUser, paginationMetaData);
    }

    public async Task<Transaction> GetTransactionsByIdAsync(Guid transactionID)
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
            .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
               .ThenInclude(p => p.ProductImages)
            .FirstOrDefaultAsync(t => t.Id.Equals(transactionID));
    }
}
