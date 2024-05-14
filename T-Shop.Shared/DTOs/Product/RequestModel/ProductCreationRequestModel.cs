namespace T_Shop.Shared.DTOs.Product.RequestModel;
public record ProductCreationRequestModel
{
    public Guid ModelID { get; set; }
    public Guid ColorID { get; set; }
    public string? Description { get; set; }
}
