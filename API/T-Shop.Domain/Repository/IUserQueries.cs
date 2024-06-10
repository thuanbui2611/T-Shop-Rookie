namespace T_Shop.Domain.Repository;
public interface IUserQueries
{
    Task<bool> CheckIfUserExisted(Guid userID);
}
