using Alteration.Application.Infrastructure.Validations;

namespace Alteration.Application.Infrastructure.Errors
{
    internal class AlterationApplicationValidationError : AlterationApplicationError<IReadOnlyCollection<ValidationErrorDetail>>
    {
        private AlterationApplicationValidationError(string message, IReadOnlyCollection<ValidationErrorDetail> details) : base(message, details)
        {
        }

        public static AlterationApplicationValidationError Create(params ValidationErrorDetail[] details)
        {
            return Create(details as IReadOnlyCollection<ValidationErrorDetail>);
        }

        public static AlterationApplicationValidationError Create(IReadOnlyCollection<ValidationErrorDetail> details)
        {
            string message;
            if (details.Count == 1)
            {
                message = details.First().Message;
            }
            else
            {
                var messages = details.Select(x => x.Message).ToList();
                messages.Insert(0, "Invalid input was provided:");
                message = string.Join("\n", messages);
            }
            return new AlterationApplicationValidationError(message, details);
        }
    }
}
