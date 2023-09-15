using FluentValidation;

namespace Alteration.Application.Features.AlterationCompleted
{
    internal class AlterationCompletedCommandValidator : AbstractValidator<AlterationCompletedCommand>
    {
        public AlterationCompletedCommandValidator() 
        {
            RuleFor(c => c.AlterationFormId).NotEmpty();
        }
    }
}
