namespace T_Shop.Shared.DTOs.User.RequestModels;
public class LockUserRequestModel
{
    public Guid UserID { get; set; }
    public bool IsLockUser { get; set; }
}
