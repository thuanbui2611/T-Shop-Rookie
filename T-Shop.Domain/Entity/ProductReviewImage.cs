using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class ProductReviewImage
{
    public Guid ImageID { get; set; } = Guid.NewGuid();
    [Column("FK_product_review_id")]
    public Guid ProductReviewID { get; set; }
    public required string ImageUrl { get; set; }
    public virtual ProductReview ProductReview { get; set; }
}
