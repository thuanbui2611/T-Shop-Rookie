namespace T_Shop.Domain.Entity;
public class CartItem : BaseModel
{
    public Guid ProductID { get; set; }
    public Guid CartID { get; set; }
    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; }
    public virtual Product Product { get; set; }
}
