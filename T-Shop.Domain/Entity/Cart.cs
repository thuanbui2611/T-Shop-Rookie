using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class Cart : BaseModel
{
    [Column("FK_user_id")]
    public Guid UserID { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }
}
