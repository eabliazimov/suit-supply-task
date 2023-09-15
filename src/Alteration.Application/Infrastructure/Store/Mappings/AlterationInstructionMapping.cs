using Alteration.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alteration.Application.Infrastructure.Store.Mappings
{
    internal class AlterationInstructionMapping : IEntityTypeConfiguration<AlterationInstruction>
    {
        public void Configure(EntityTypeBuilder<AlterationInstruction> builder)
        {
            builder.HasKey(ai => ai.Id);
            builder
                .HasOne(ai => ai.AlterationForm)
                .WithMany(af => af.AlterationInstructions)
                .HasForeignKey(ai => ai.AlterationFormId);
        }
    }
}
