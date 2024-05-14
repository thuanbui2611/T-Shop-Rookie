namespace T_Shop.Domain.Entity;
public class Model : BaseModel
{
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid BrandID { get; set; }

    public virtual ICollection<Product> Products { get; set; }
    public virtual Brand Brand { get; set; } = new Brand();
}
