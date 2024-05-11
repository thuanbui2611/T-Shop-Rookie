namespace T_Shop.Domain.Entity.Exceptions
{
    public class NotFoundException : Exception
    {
        protected NotFoundException(string message)
        : base(message)
        { }
    }
}
