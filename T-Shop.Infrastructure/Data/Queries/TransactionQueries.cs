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
    public async Task<Transaction?> GetTransactionByPaymentIntentId(string paymentIntentID)
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
            .OrderByDescending(t => t.CreatedAt)
            .Skip(pagination.pageSize * (pagination.pageNumber - 1))
            .Take(pagination.pageSize)
            .ToListAsync();

        return (transactionsOfUser, paginationMetaData);
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid transactionID, bool trackChanges)
    {
        return trackChanges ?
            await dbSet
            .AsTracking()
            .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.ProductReview)
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
            .FirstOrDefaultAsync(t => t.Id.Equals(transactionID))
            :
            await dbSet
           .Include(t => t.Order)
               .ThenInclude(o => o.OrderDetails)
               .ThenInclude(od => od.ProductReview)
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

    //public async Task<TransactionWithCustomerResponseModel?> GetTransactionWithCustomerByIdAsync(Guid transactionID)
    //{
    //    return
    //        await dbSet
    //        .Include(t => t.Order)
    //           .ThenInclude(o => o.OrderDetails)
    //           .ThenInclude(od => od.ProductReview)
    //       .Include(t => t.Order)
    //           .ThenInclude(o => o.OrderDetails)
    //           .ThenInclude(od => od.Product)
    //           .ThenInclude(p => p.Color)
    //       .Include(t => t.Order)
    //           .ThenInclude(o => o.OrderDetails)
    //           .ThenInclude(od => od.Product)
    //           .ThenInclude(p => p.Type)
    //       .Include(t => t.Order)
    //           .ThenInclude(o => o.OrderDetails)
    //           .ThenInclude(od => od.Product)
    //           .ThenInclude(p => p.Model)
    //           .ThenInclude(m => m.Brand)
    //        .Include(t => t.Order)
    //           .ThenInclude(o => o.OrderDetails)
    //           .ThenInclude(od => od.Product)
    //           .ThenInclude(p => p.ProductImages)
    //        .Join(
    //            _userManager.Users, t => t.CustomerID, u => u.Id,
    //            (transactionTable, userTable) => new TransactionWithCustomerResponseModel()
    //            {
    //                ID = transactionTable.Id,
    //                Reason = transactionTable.Reason,
    //                Status = transactionTable.Status,
    //                CreatedAt = transactionTable.CreatedAt,
    //                User = new Shared.DTOs.User.ResponseModels.UserResponseModel()
    //                {
    //                    Id = userTable.Id,
    //                    Address = userTable.Address,
    //                    Avatar = userTable.Avatar,
    //                    Date_of_birth = userTable.DateOfBirth,
    //                    Email = userTable.Email,
    //                    Full_name = userTable.FullName,
    //                    Gender = userTable.Gender,
    //                    Is_locked = userTable.IsLocked,
    //                    PhoneNumber = userTable.PhoneNumber,
    //                    Username = userTable.UserName
    //                },
    //                Order = new OrderResponseModel()
    //                {
    //                    ID = transactionTable.Order.Id,
    //                    ShippingAddress = transactionTable.Order.ShippingAddress,
    //                    ClientSecret = transactionTable.Order.ClientSecret,
    //                    CustomerID = transactionTable.Order.UserID,
    //                    OrderDetails = new List<OrderDetailResponseModel>()
    //                    {

    //                    }
    //                },

    //            })
    //        .FirstOrDefaultAsync(t => t.Transaction.Id == transactionID);
    //}
}
