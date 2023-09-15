using MediatR;

namespace Alteration.Application.Features.AlterationCompleted
{
    public record class AlterationCompletedCommand(Guid AlterationFormId) : IRequest
    {
    }
}
