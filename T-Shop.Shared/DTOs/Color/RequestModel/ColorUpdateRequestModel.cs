using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.Color.RequestModel;
public class ColorUpdateRequestModel
{
    [JsonIgnore]
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string HexColor { get; set; }
}
