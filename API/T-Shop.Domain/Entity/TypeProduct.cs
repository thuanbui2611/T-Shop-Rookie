namespace T_Shop.Domain.Entity;
public class TypeProduct : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}
