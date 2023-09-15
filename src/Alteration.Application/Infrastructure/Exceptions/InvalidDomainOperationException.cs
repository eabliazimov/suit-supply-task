namespace Alteration.Application.Infrastructure.Exceptions
{
    public class InvalidDomainOperationException: Exception
    {
        public InvalidDomainOperationException() { }
        public InvalidDomainOperationException(string message) : base(message) { }
    }
}
