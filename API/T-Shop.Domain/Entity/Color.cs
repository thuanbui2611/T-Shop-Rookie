namespace T_Shop.Domain.Entity;
public class Color : BaseModel
{
    public string Name { get; set; }
    public string HexColor { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
