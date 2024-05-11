using System.Text.Json;
using System.Text.Json.Serialization;

namespace T_Shop.Application.Common.Exceptions
{
    public class ErrorResponseModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
