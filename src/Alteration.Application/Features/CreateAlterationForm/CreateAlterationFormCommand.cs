using Alteration.Application.Domain;
using MediatR;

namespace Alteration.Application.Features.CreateAlterationForm
{
    public record class CreateAlterationFormCommand: IRequest
    {
        public string? SuitReference { get; init; }
        public CreateAlterationFormAlterationInstruction[]? AlterationInstructions { get; init; }
    }

    public record class CreateAlterationFormAlterationInstruction(AlterationTypes AlterationType, double AlterationLength);
}
