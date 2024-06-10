using Microsoft.AspNetCore.Http;

namespace T_Shop.Shared.DTOs.Product.RequestModel;
public record ProductCreationRequestModel
{
    public Guid ModelID { get; set; }
    public Guid ColorID { get; set; }
    public Guid TypeID { get; set; }
    public required string Variant { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public required IFormFileCollection ImagesUpload { get; set; }
}
