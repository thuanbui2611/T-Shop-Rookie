namespace T_Shop.Domain.Exceptions;
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
    : base(message)
    {
    }
}
