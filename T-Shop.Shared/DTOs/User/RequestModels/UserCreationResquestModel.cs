namespace T_Shop.Shared.DTOs.User.RequestModels;
public class UserCreationResquestModel
{
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public DateTime Date_of_birth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
