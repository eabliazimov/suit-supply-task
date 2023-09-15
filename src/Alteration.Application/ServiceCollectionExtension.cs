using System.Reflection;
using Alteration.Application.Infrastructure.Services;
using Alteration.Application.Infrastructure.Store;
using Alteration.Application.Infrastructure.Validations;
using MediatR.Pipeline;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alteration.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddAlterationApplication(
            this IServiceCollection serviceCollection, 
            Action<ApplicationOptions> configure)
        {
            var options = new ApplicationOptions();
            configure.Invoke(options);
            options.AssertIsValid();

            serviceCollection.AddSingleton<IClock>(SystemClock.Instance);
            serviceCollection.AddTransient<IEmailNotificationService, EmailNotificationService>();
            serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            serviceCollection.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            serviceCollection.AddAlterationSqlServiceDbContext(options.SqlServerOptions);
            serviceCollection.AddFluentValidation();
        }
    }
}
