using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Shop.Domain.Entity;

namespace T_Shop.Infrastructure.Persistence.Configurations;
public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.ToTable("table_product_review");

        //Product Review - Product
        builder.HasOne(pr => pr.Product)
            .WithMany(p => p.ProductReviews)
            .HasForeignKey(p => p.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

        //Product Review - OrderDetail
        builder.HasOne(pr => pr.OrderDetail)
            .WithOne(t => t.ProductReview)
            .HasForeignKey<ProductReview>(pr => new { pr.OrderID, pr.ProductID })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
