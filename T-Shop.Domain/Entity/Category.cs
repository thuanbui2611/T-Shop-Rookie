namespace T_Shop.Domain.Entity
{
    public class Category : BaseModel
    {
        public string? Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
