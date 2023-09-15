using Alteration.Application.Domain;
using Microsoft.EntityFrameworkCore;

namespace Alteration.Application.Infrastructure.Store
{
    public interface IAlterationApplicationDbContext
    {
        DbSet<AlterationForm> AlterationForms { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
