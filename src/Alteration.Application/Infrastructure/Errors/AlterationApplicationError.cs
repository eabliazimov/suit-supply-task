using Alteration.Application.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Alteration.Application.Infrastructure.Errors
{
    public abstract class AlterationApplicationError
    {
        [JsonConstructor]
        protected AlterationApplicationError(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public AlterationApplicationException ToException()
        {
            return new AlterationApplicationException(this);
        }
    }

    internal abstract class AlterationApplicationError<T> : AlterationApplicationError
    {
        protected AlterationApplicationError(string message, T details) : base(message)
        {
            Details = details;
        }

        public T Details { get; }
    }
}
