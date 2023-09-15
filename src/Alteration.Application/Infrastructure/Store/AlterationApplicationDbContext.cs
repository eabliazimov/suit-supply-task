using System.Reflection;
using Alteration.Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Infrastructure.Store
{
    internal class AlterationApplicationDbContext : DbContext, IAlterationApplicationDbContext
    {
        public AlterationApplicationDbContext(DbContextOptions<AlterationApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<AlterationForm> AlterationForms { get; private set; }

        private DbSet<AlterationInstruction> AlterationInstructions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
