using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.Type.RequestModel;
public class TypeUpdateRequestModel
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
}
