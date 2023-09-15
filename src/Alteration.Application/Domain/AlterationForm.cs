using Alteration.Application.Infrastructure.Exceptions;
using System.Threading;

namespace Alteration.Application.Domain
{
    public class AlterationForm
    {
        private AlterationForm() { }

        public required Guid Id { get; init; }
        public int Status { get; private set; }
        public required string SuitId { get; init; }
        public required DateTimeOffset CreatedOn { get; init; }

        public ICollection<AlterationInstruction> AlterationInstructions { get; init; }

        public static AlterationForm Create(
            string suitId,
            ICollection<AlterationInstruction> alterationInstructions,
            IClock clock)
        {
            return new AlterationForm()
            {
                Id = Guid.NewGuid(),
                Status = (int)AlterationFormStatuses.Created,
                CreatedOn = clock.UtcNow,
                SuitId = suitId,
                AlterationInstructions = alterationInstructions
            };
        }

        public void ChangeStatusToPaid()
        {
            switch ((AlterationFormStatuses)Status)
            {
                case AlterationFormStatuses.Created:
                    Status = (int)AlterationFormStatuses.Paid;
                    break;

                default: throw new InvalidDomainOperationException($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.Created)} status.");
            }

        }

        public void ChangeStatusToAlterationStarted()
        {
            switch ((AlterationFormStatuses)Status)
            {
                case AlterationFormStatuses.Paid:
                    Status = (int)AlterationFormStatuses.AlterationStarted;
                    break;

                default: throw new InvalidDomainOperationException($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.Paid)} status.");
            }

        }

        public void ChangeStatusToAlterationFinished()
        {
            switch ((AlterationFormStatuses)Status)
            {
                case AlterationFormStatuses.AlterationStarted:
                    {
                        Status = (int)AlterationFormStatuses.AlterationFinished;
                    }
                    break;

                default: throw new InvalidDomainOperationException($"Alteration Form is not in {Enum.GetName(AlterationFormStatuses.AlterationStarted)} status.");
            }
        }
    }
}
