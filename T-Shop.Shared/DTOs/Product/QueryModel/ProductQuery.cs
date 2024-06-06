namespace T_Shop.Shared.DTOs.Product.QueryModel;
public class ProductQuery
{
    public List<string>? Types { get; set; }
    public List<string>? Brands { get; set; }
    public List<string>? Models { get; set; }
    public List<string>? Colors { get; set; }
    public string? Search { get; set; }
    public bool? IsOnStock { get; set; } = null;
}
