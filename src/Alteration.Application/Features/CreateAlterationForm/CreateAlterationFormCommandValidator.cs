using FluentValidation;

namespace Alteration.Application.Features.CreateAlterationForm
{
    internal class CreateAlterationFormCommandValidator : AbstractValidator<CreateAlterationFormCommand>
    {
        public CreateAlterationFormCommandValidator() 
        {
            RuleFor(c => c.SuitReference).NotEmpty();
            RuleForEach(c => c.AlterationInstructions)
                .Must(i => i.AlterationLength >= -5 && i.AlterationLength <= 5)
                .WithMessage("Alteration Length in instructions must be in range [-5;5]");
            RuleForEach(c => c.AlterationInstructions)
                .Must(i => i.AlterationType > 0)
                .WithMessage("Alteration Type must be set for each instruction");
            RuleFor(c => c.AlterationInstructions).NotEmpty().WithMessage("No Alteration Instructions provided.");
        }
    }
}
