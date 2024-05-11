using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity
{
    public class Product : BaseModel
    {
        // guard class
        // new colection = []
        // reshaper
        [Required(ErrorMessage = "Product name is a required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters")]
        public string? Name { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length for the description is 100 characters")]
        public string Description { get; set; } = string.Empty;

        // Navigation property
        [Column("FK_category_id")]
        [Required(ErrorMessage = "Product category is a required.")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = new Category();
    }
}
