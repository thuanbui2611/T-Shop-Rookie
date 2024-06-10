using T_Shop.Shared.DTOs.Product.ResponseModel;
using T_Shop.Shared.DTOs.ProductReview.ResponseModel;

namespace T_Shop.Shared.DTOs.Order.ResponseModel;
public class OrderResponseModel
{
    public Guid ID { get; set; }
    public Guid CustomerID { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentIntentID { get; set; }
    public string ClientSecret { get; set; }
    public List<OrderDetailResponseModel> OrderDetails { get; set; }
}

public class OrderDetailResponseModel
{
    public ProductOfOrderResponseModel Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ProductReviewResponseModel ProductReview { get; set; }
}

public class ProductOfOrderResponseModel : ProductResponseModel
{
    public bool IsReviewed { get; set; } = false;
}