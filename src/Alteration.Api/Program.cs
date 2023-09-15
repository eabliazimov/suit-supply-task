using Microsoft.AspNetCore.Builder;
using Alteration.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Serilog;
using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Hosting;

namespace Alteration.Api
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddSwaggerGen();
                builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
                builder.Services
                    .AddControllers(opt => opt.ModelValidatorProviders.Clear())
                    .AddNewtonsoftJson(opts =>
                    {
                        opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });
                builder.Services.AddAlterationApplication(opts => builder.Configuration.Bind(opts));

                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsights(
                            services.GetRequiredService<TelemetryConfiguration>(),
                            TelemetryConverter.Traces)
                    .Enrich.FromLogContext());

                var app = builder.Build();
                app.UseCors(b =>
                    b.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

                app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseHttpsRedirection();
                app.UseSerilogRequestLogging();
                app.UseRouting();
                app.MapControllers();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                        options.RoutePrefix = string.Empty;
                    });
                }

                app.Run();
            } catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
