using Alteration.Application.Infrastructure.Exceptions;
using Alteration.Application.Infrastructure.Store;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Features.PaymentReceived
{
    public class PaymentReceivedCommandHandler : IRequestHandler<PaymentReceivedCommand>
    {
        private readonly IAlterationApplicationDbContext _alterationApplicationDbContext;

        public PaymentReceivedCommandHandler(IAlterationApplicationDbContext alterationApplicationDbContext)
        {
            _alterationApplicationDbContext = alterationApplicationDbContext;
        }

        public async Task Handle(PaymentReceivedCommand request, CancellationToken cancellationToken)
        {
            var alterationForm = await _alterationApplicationDbContext.AlterationForms.FirstOrDefaultAsync(af => af.Id == request.AlterationFormId, cancellationToken);

            if (alterationForm == null)
                throw new ResourceNotFoundException($"Alteration Form with id: {request.AlterationFormId} could not be find.");

            alterationForm.ChangeStatusToPaid();

            await _alterationApplicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
