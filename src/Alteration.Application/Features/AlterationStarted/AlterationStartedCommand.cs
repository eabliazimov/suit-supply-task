using MediatR;

namespace Alteration.Application.Features.AlterationStarted
{
    public record class AlterationStartedCommand(Guid AlterationFormId) : IRequest;
}
