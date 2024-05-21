namespace T_Shop.Domain.Entity;
public class ProductReview : BaseModel
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public Guid TransactionID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Product Product { get; set; }
    public virtual Transaction Transaction { get; set; }
    public virtual ICollection<ProductReviewImage> ProductReviewImages { get; set; } = [];

}
