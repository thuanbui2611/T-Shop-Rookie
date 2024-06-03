using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class Order : BaseModel
{
    [Column("FK_user_id")]
    public Guid UserID { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentIntentID { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public bool IsPayment { get; set; } = false;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];
    public virtual Transaction Transaction { get; set; }
}
