namespace T_Shop.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        : base(message)
        { }
    }
}
