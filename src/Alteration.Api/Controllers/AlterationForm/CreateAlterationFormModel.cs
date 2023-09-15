using System.Linq;
using Alteration.Application.Domain;
using Alteration.Application.Features.CreateAlterationForm;

namespace Alteration.Api.Controllers.AlterationForm
{
    public record class AlterationInstruction(AlterationTypes AlterationType, double AlterationLength);
    public record class CreateAlterationFormModel(string? SuitReference, AlterationInstruction[]? AlterationInstructions)
    {
        public CreateAlterationFormCommand ToCreateAlterationFormCommand()
        {
            return new CreateAlterationFormCommand()
            {
                SuitReference = SuitReference,
                AlterationInstructions = AlterationInstructions?
                    .Select(ai => new CreateAlterationFormAlterationInstruction(ai.AlterationType, ai.AlterationLength)).ToArray()
            };
        } 
    }
}
