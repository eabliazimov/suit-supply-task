using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Alteration.Application.Infrastructure.Validations
{
    internal static class FluentValidationExtensions
    {
        public static ValidationErrorDetail MapToValidationErrorDetail(this ValidationFailure failure)
        {
            var key = failure.PropertyName;
            return new ValidationErrorDetail(key, failure.ErrorMessage);
        }

        public static void AddFluentValidation(this IServiceCollection services)
        {
            var scanner = new AssemblyScanner(AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()));
            scanner.ForEach(result => services.AddTransient(result.InterfaceType, result.ValidatorType));
            services.AddSingleton(typeof(IValidator<>), typeof(EmptyValidator<>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));
        }
    }
}
