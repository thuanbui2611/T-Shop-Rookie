using T_Shop.Shared.DTOs.Product.ResponseModel;

namespace T_Shop.Shared.DTOs.Cart.ResponseModel;
public class CartResponseModel
{
    public Guid ID { get; set; }
    public Guid UserId { get; set; }
    public List<CartItemResponseModel> CartItems { get; set; }
}

public class CartItemResponseModel
{
    public ProductResponseModel Product { get; set; }
    public int Quantity { get; set; }
}

