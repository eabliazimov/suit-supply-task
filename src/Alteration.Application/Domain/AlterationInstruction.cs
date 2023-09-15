namespace Alteration.Application.Domain
{
    public record class AlterationInstruction 
    {
        private AlterationInstruction() {}

        public required Guid Id { get; init; }
        public required int AlterationTypeId { get; init; }
        public required double AlterationLength { get; init; }

        public Guid AlterationFormId { get; private set; }
        public AlterationForm AlterationForm { get; private set; }

        public static AlterationInstruction Create(
            AlterationTypes alterationType,
            double alterationLength)
        {
            return new AlterationInstruction()
            {
                Id = Guid.NewGuid(),
                AlterationTypeId = (int) alterationType,
                AlterationLength = alterationLength
            };
        }
    }
}
