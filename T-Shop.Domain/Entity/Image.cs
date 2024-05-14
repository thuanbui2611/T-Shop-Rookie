namespace T_Shop.Domain.Entity;
public class Image : BaseModel
{
    public string ImageURL { get; set; }
    public string PublicID { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; }
}
