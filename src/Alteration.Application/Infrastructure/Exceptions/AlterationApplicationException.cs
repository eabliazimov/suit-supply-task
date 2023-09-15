using Alteration.Application.Infrastructure.Errors;

namespace Alteration.Application.Infrastructure.Exceptions
{
    public class AlterationApplicationException : Exception
    {
        public AlterationApplicationException(AlterationApplicationError error) : base(error?.Message)
        {
            Error = error!;
        }

        public AlterationApplicationException(AlterationApplicationError error, Exception inner) : base(error.Message, inner)
        {
            Error = error!;
        }

        public AlterationApplicationError Error { get; }
    }
}
