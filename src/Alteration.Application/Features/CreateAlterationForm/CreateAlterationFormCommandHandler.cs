using Alteration.Application.Domain;
using Alteration.Application.Infrastructure.Store;
using MediatR;

namespace Alteration.Application.Features.CreateAlterationForm
{
    public class CreateAlterationFormCommandHandler : IRequestHandler<CreateAlterationFormCommand>
    {
        private readonly IAlterationApplicationDbContext _alterationApplicationDbContext;
        private readonly IClock _clock;

        public CreateAlterationFormCommandHandler(
            IAlterationApplicationDbContext alterationApplicationDbContext,
            IClock clock)
        {
            _alterationApplicationDbContext = alterationApplicationDbContext;
            _clock = clock;
        }

        public async Task Handle(CreateAlterationFormCommand request, CancellationToken cancellationToken)
        {
            var alterationForm = AlterationForm.Create(
                request.SuitReference, 
                request.AlterationInstructions
                    .Select(cafai => 
                        AlterationInstruction.Create(cafai.AlterationType, cafai.AlterationLength)).ToList(), _clock);

            _alterationApplicationDbContext.AlterationForms.Add(alterationForm);
            await _alterationApplicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
