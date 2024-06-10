using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("table_order");

        //Order - Transaction
        builder.HasOne(o => o.Transaction)
            .WithOne(t => t.Order)
            .HasForeignKey<Transaction>(t => t.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
