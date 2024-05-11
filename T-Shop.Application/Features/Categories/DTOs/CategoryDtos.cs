using System.Text.Json.Serialization;

namespace T_Shop.Application.Features.Categories.DTOs
{
    public class CategoryDtos
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
