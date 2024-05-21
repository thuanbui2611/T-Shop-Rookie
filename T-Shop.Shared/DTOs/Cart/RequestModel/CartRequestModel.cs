using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.Cart.RequestModel;
public class CartRequestModel
{
    [JsonIgnore]
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Quantity { get; set; }

}
