using Alteration.Application.Infrastructure.Errors;
using Alteration.Application.Infrastructure.Exceptions;
using FluentValidation;
using MediatR.Pipeline;

namespace Alteration.Application.Infrastructure.Validations
{
    internal class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationPreProcessor(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return;
            }

            var details = validationResult.Errors.Select(x => FluentValidationExtensions.MapToValidationErrorDetail(x)).ToArray();
            var error = AlterationApplicationValidationError.Create(details);
            throw new AlterationApplicationException(error);
        }
    }
}
