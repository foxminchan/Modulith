using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Domain.Events;
using Modulith.Persistence.Interceptors;
using Polly;
using Serilog;

namespace Modulith.Persistence;

public static class Extension
{
    public static IServiceCollection AddAppDbContext<TDbContext>(
        this IServiceCollection services,
        string connString,
        IModel model)
        where TDbContext : DbContext, IDatabaseFacade, IDomainEventContext

    {
        services.AddDbContextPool<TDbContext>((sp, options) =>
        {
            options.UseSqlServer(connString, sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                            null);
                    }
                )
                .UseModel(model)
                .UseExceptionProcessor()
                .EnableServiceProviderCaching()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
        });

        services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<TDbContext>());
        services.AddScoped<IDomainEventContext>(p => p.GetRequiredService<TDbContext>());

        return services;
    }
    
    public static void ApplyDatabaseMigration<TDbContext>(this WebApplication app, string connString)
        where TDbContext : DbContext, IDatabaseFacade, IDomainEventContext
    {
        using var scope = app.Services.CreateScope();
        var retryPolicy = CreateRetryPolicy(connString, Log.Logger, app.Configuration);
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        retryPolicy.Execute(context.Database.Migrate);
    }

    private static Policy CreateRetryPolicy(string connString, ILogger logger, IConfiguration configuration)
    {
        if (bool.TryParse(configuration["RetryMigrations"], out _))
            return Policy.Handle<Exception>().WaitAndRetryForever(
                _ => TimeSpan.FromSeconds(5),
                (exception, retry, _) =>
                {
                    logger.Warning(
                        exception,
                        "[{Prefix}] Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {Retry}, connection {Connection})",
                        nameof(ApplyDatabaseMigration),
                        exception.GetType().Name,
                        exception.Message,
                        retry,
                        connString);
                }
            );

        return Policy.NoOp();
    }
}