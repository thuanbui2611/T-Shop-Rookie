using System.Text.Json.Serialization;

namespace T_Shop.Shared.DTOs.User.ResponseModels;
public class UserResponseModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("full_name")]
    public string? Full_name { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTime? Date_of_birth { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("is_locked")]
    public string? Is_locked { get; set; }
    [JsonPropertyName("role")]
    public string? Role { get; set; }

}
