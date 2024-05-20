using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("table_cart_items");

        builder.HasKey(ci => new { ci.CartID, ci.ProductID });

        //CartItems - Cart
        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(c => c.CartID)
            .OnDelete(DeleteBehavior.Cascade);

        //CartItems - Product
        builder.HasOne(ci => ci.Product)
            .WithMany(p => p.CartProducts)
            .HasForeignKey(p => p.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
