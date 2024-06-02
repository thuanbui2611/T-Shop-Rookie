using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class Transaction : BaseModel
{
    [Column("FK_customer_id")]
    public Guid CustomerID { get; set; }
    [Column("FK_order_id")]
    public Guid OrderID { get; set; }
    public string Status { get; set; } = string.Empty; //In Process or Success or Canceled
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Order Order { get; set; }
    //public virtual ProductReview ProductReview { get; set; }
}
