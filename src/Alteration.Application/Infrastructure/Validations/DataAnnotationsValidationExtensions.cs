using System.ComponentModel.DataAnnotations;

namespace Alteration.Application.Infrastructure.Validations
{
    public static class DataAnnotationsValidationExtensions
    {
        public static void AssertIsValid(this object validatedObject)
        {
            Validator.ValidateObject(
                validatedObject, 
                new ValidationContext(validatedObject), 
                true);
        }
    }
}
