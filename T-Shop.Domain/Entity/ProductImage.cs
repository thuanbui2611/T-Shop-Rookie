namespace T_Shop.Domain.Entity;
public class ProductImage
{
    public Guid ImageID { get; set; } = Guid.NewGuid();
    public Guid ProductID { get; set; }
    public required string ImageUrl { get; set; }
    public bool IsMain { get; set; } = false;

    public virtual Product Product { get; set; }
}
