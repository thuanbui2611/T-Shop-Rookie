namespace T_Shop.Shared.DTOs.Order.RequestModel;
public class OrderRequestModel
{
    public Guid UserID { get; set; }
    public string ShippingAddress { get; set; }
    public List<ProductOfOrder> Products { get; set; }
}

public class ProductOfOrder
{
    public Guid ProductID { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}