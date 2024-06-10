using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ProductReviewImageConfiguration : IEntityTypeConfiguration<ProductReviewImage>
{
    public void Configure(EntityTypeBuilder<ProductReviewImage> builder)
    {
        builder.ToTable("table_product_review_image");

        builder.HasKey(p => p.ImageID);

        //Product Review Image - Product Review
        builder.HasOne(pri => pri.ProductReview)
            .WithMany(pr => pr.ProductReviewImages)
            .HasForeignKey(pr => pr.ProductReviewID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
