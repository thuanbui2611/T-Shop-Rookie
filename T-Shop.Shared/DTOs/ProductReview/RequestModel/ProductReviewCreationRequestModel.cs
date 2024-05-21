using Microsoft.AspNetCore.Http;

namespace T_Shop.Shared.DTOs.ProductReview.RequestModel;
public class ProductReviewCreationRequestModel
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public Guid TransactionID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public IFormFileCollection ImagesUpload { get; set; }
}
