using MediatR;

namespace Alteration.Application.Features.PaymentReceived
{
    public record class PaymentReceivedCommand(Guid AlterationFormId): IRequest;
}
