using Alteration.Application.Domain;

namespace Alteration.Application.Features.GetAlterationFormsQuery
{
    public record class GetAlterationFormsQueryResponse(
        Guid Id,
        int Status,
        string SuitId,
        DateTimeOffset CreatedOn)
    {
        public required IReadOnlyCollection<GetAlterationFormsQueryResponseAlterationInstruction> AlterationInstructions { get; init; }
    }

    public record class GetAlterationFormsQueryResponseAlterationInstruction(
        Guid Id,
        AlterationTypes AlterationType,
        double AlterationLength);
}
