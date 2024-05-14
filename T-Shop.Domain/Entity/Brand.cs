namespace T_Shop.Domain.Entity;
public class Brand : BaseModel
{
    public string Name { get; set; }
    public Guid TypeID { get; set; }
    public virtual TypeProduct Type { get; set; }
    public virtual ICollection<Model> Models { get; set; }

}
