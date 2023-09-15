using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Services;
using Alteration.Application.Infrastructure.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Features.AlterationCompleted
{
    public class AlterationCompletedCommandHandler : IRequestHandler<AlterationCompletedCommand>
    {
        private readonly IAlterationApplicationDbContext _alterationApplicationDbContext;
        private readonly IEmailNotificationService _emailNotificationService;

        public AlterationCompletedCommandHandler(
            IAlterationApplicationDbContext alterationApplicationDbContext,
            IEmailNotificationService emailNotificationService)
        {
            _alterationApplicationDbContext = alterationApplicationDbContext;
            _emailNotificationService = emailNotificationService;
        }

        public async Task Handle(AlterationCompletedCommand request, CancellationToken cancellationToken)
        {
            var alterationForm = await _alterationApplicationDbContext.AlterationForms.FirstOrDefaultAsync(af => af.Id == request.AlterationFormId, cancellationToken);

            if (alterationForm == null)
                throw new ResourceNotFoundException($"Alteration Form with id: {request.AlterationFormId} could not be find.");


            alterationForm.ChangeStatusToAlterationFinished();
            await _emailNotificationService.NotifyCustomerAsync(alterationForm.Id, cancellationToken);
            await _alterationApplicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
