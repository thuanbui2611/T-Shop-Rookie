using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("table_order_detail");

        builder.HasKey(od => new { od.OrderID, od.ProductID });

        //OrderDetail - Order
        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        //OrderDetail - Product
        builder.HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(p => p.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
