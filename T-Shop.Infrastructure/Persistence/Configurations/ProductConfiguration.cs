using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("table_product");
            builder.HasIndex(p => new
            {
                p.ColorID,
                p.ModelID,
                p.Variant
            })
                .IsUnique(true);


            // Product - Model
            builder.HasOne(p => p.Model)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ModelID)
                .OnDelete(DeleteBehavior.Cascade);

            // Product - Color
            builder.HasOne(p => p.Color)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ColorID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
