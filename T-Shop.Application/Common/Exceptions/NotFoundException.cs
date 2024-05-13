namespace T_Shop.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(string message)
        : base(message)
        { }
    }
}
