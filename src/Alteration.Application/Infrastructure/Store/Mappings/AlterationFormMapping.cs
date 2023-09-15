using Alteration.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alteration.Application.Infrastructure.Store.Mappings
{
    internal class AlterationFormMapping : IEntityTypeConfiguration<AlterationForm>
    {
        public void Configure(EntityTypeBuilder<AlterationForm> builder)
        {
            builder.HasKey(af => af.Id);
            builder
                .HasMany(af => af.AlterationInstructions)
                .WithOne(ai => ai.AlterationForm)
                .HasForeignKey(ai => ai.AlterationFormId);

            builder
                .HasIndex(af => af.SuitId)
                .IsUnique(false);
        }
    }
}
