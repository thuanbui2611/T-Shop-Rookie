using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("table_product_image");

        builder.HasKey(p => p.ImageID);

        //Image - Product
        builder.HasOne(p => p.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(p => p.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
