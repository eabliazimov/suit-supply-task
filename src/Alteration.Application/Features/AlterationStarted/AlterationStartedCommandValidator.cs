using FluentValidation;

namespace Alteration.Application.Features.AlterationStarted
{
    internal class AlterationStartedCommandValidator : AbstractValidator<AlterationStartedCommand>
    {
        public AlterationStartedCommandValidator()
        {
            RuleFor(c => c.AlterationFormId).NotEmpty();
        }
    }
}
