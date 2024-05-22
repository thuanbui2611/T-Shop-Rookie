using T_Shop.Shared.DTOs.Product.ResponseModel;

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
    public ProductResponseModel Product { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
