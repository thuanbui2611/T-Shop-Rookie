using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class ProductImage
{
    public Guid ImageID { get; set; } = Guid.NewGuid();
    [Column("FK_product_id")]
    public Guid ProductID { get; set; }
    public string ImagePublicID { get; set; }
    public bool IsMain { get; set; } = false;
    public virtual Product Product { get; set; }
}
