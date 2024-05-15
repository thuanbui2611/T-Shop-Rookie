using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity
{
    public class Product : BaseModel
    {
        [Column("FK_model_id")]
        public Guid ModelID { get; set; }
        [Column("FK_color_id")]
        public Guid ColorID { get; set; }
        [Column("FK_type_id")]
        public Guid TypeID { get; set; }
        public string Configuration { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsOnStock { get; set; }

        public virtual TypeProduct Type { get; set; }
        public virtual Color Color { get; set; }
        public virtual Model Model { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
