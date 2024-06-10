namespace T_Shop.Shared.DTOs.Cart.RequestModel;
public class CartItemDeleteRequestModel
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
}
