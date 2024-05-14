namespace T_Shop.Shared.DTOs.Product.RequestModel;
public class ProductUpdateRequestModel
{
    public Guid Id { get; set; }
    public Guid ModelID { get; set; }
    public Guid ColorID { get; set; }
    public string? Description { get; set; }
}
