using Microsoft.AspNetCore.Http;

namespace T_Shop.Shared.DTOs.User.RequestModels;
public class UserUpdateRequestModel
{
    public Guid ID { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public IFormFile? AvatarUpload { get; set; }
}
