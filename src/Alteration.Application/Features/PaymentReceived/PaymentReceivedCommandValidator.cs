using FluentValidation;

namespace Alteration.Application.Features.PaymentReceived
{
    internal class PaymentReceivedCommandValidator : AbstractValidator<PaymentReceivedCommand>
    {
        public PaymentReceivedCommandValidator()
        {
            RuleFor(c => c.AlterationFormId).NotEmpty();
        }
    }
}
