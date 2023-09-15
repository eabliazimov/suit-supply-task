namespace Alteration.Application.Infrastructure.Validations
{
    public class ValidationErrorDetail
    {
        public ValidationErrorDetail(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }

        public string Message { get; }
    }
}
