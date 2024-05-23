using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using T_Shop.Shared.DTOs.Product.ResponseModel;
namespace T_Shop.Shared.DTOs.Product.RequestModel;
public class ProductUpdateRequestModel
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public Guid ModelID { get; set; }
    public Guid ColorID { get; set; }
    public Guid TypeID { get; set; }
    public string Variant { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string Images { get; set; }
    public List<ImageOfProductResponseModel>? ImagesList
    {
        get
        {
            var images = JsonSerializer.Deserialize<List<ImageOfProductResponseModel>>(this.Images);
            return images;
        }
        set { }
    }

    public IFormFileCollection? ImagesUpload { get; set; }
}


