namespace T_Shop.Shared.DTOs.User.ResponseModels;
public class UserResponseModel
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Avatar { get; set; }
    public bool? IsLocked { get; set; }
    public string? Role { get; set; }

}
