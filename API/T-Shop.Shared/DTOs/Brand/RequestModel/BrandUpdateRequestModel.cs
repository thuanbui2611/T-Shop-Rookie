using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.Brand.RequestModel;
public class BrandUpdateRequestModel
{
    [JsonIgnore]
    public Guid ID { get; set; }
    public string Name { get; set; }
}
