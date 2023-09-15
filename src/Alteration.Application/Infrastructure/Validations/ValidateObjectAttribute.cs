using System.ComponentModel.DataAnnotations;

namespace Alteration.Application.Infrastructure.Validations
{
    internal class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value!, null, null);

            Validator.TryValidateObject(value!, context, results, true);
            if (results.Count == 0)
            {
                return ValidationResult.Success!;
            }
            var messages = string.Join(Environment.NewLine, results.Select(x => x.ErrorMessage));
            var compositeResult = new CompositeValidationResult(
                $"Validation for {validationContext.DisplayName} failed:{Environment.NewLine}{messages}"
            );
            results.ForEach(compositeResult.AddResult);
            return compositeResult;
        }
    }
}
