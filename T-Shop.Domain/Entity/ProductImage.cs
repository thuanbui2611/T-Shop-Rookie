using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class ProductImage : BaseModel
{
    [Column("PK_FK_product_id")]
    public Guid ProductID { get; set; }
    [Column("PK_FK_image_id")]
    public Guid ImageID { get; set; }

    public virtual Product Product { get; set; }
    public virtual Image Image { get; set; }
}
