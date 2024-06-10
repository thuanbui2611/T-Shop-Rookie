using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class CartItem
{
    [Column("PK_FK_cart_id")]
    public Guid CartID { get; set; }
    [Column("PK_FK_product_id")]
    public Guid ProductID { get; set; }
    public int Quantity { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }
}
