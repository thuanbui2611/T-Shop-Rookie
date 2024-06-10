namespace T_Shop.Shared.DTOs.ProductReview.ResponseModel;
public class ProductReviewResponseModel
{
    public Guid ID { get; set; }
    public Guid ProductID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public UserOfReview User { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ProductReviewImagesResponseModel> Images { get; set; } = [];
}

public class ProductReviewImagesResponseModel
{
    public Guid ImageID { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

public class UserOfReview
{
    public Guid ID { get; set; }
    public string FullName { get; set; }
    public string Avatar { get; set; }
}

