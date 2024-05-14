namespace T_Shop.Domain.Entity;
public class TypeProduct : BaseModel
{
    public string Name { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = [];
}
