namespace T_Shop.Domain.Entity.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
        : base(message)
        {
        }
    }
}
