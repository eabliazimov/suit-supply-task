using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alteration.Application.Infrastructure.Store
{
    public static class AlterationApplicationDbContextExtensions
    {
        /// <summary>
        /// SQL Server Entity Framework Core Database Context registration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqlServiceOptions"></param>
        public static void AddAlterationSqlServiceDbContext(this IServiceCollection services, SqlServerOptions sqlServiceOptions)
        {
            services.AddDbContext<IAlterationApplicationDbContext, AlterationApplicationDbContext>(
                opts => opts.UseSqlServer(sqlServiceOptions.ConnectionString));
        }
    }
}
