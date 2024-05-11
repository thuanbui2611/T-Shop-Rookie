using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.Category
{
    public class CategoryDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
