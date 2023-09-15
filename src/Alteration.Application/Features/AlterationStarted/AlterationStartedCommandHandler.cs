using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Features.AlterationStarted
{
    public class AlterationStartedCommandHandler : IRequestHandler<AlterationStartedCommand>
    {
        private readonly IAlterationApplicationDbContext _alterationApplicationDbContext;

        public AlterationStartedCommandHandler(IAlterationApplicationDbContext alterationApplicationDbContext)
        {
            _alterationApplicationDbContext = alterationApplicationDbContext;
        }

        public async Task Handle(AlterationStartedCommand request, CancellationToken cancellationToken)
        {
            var alterationForm = await _alterationApplicationDbContext.AlterationForms.FirstOrDefaultAsync(af => af.Id == request.AlterationFormId, cancellationToken);

            if (alterationForm == null)
                throw new ResourceNotFoundException($"Alteration Form with id: {request.AlterationFormId} could not be find.");

            alterationForm.ChangeStatusToAlterationStarted();

            await _alterationApplicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
