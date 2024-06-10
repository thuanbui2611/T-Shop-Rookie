using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.ModelProduct.RequestModel;
public class ModelUpdateRequestModel
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public Guid BrandID { get; set; }
}
