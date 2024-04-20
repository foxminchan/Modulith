﻿using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Persistence.Interceptors;
using Modulith.SharedKernel.Events;

namespace Modulith.Persistence;

public static class Extension
{
    public static IServiceCollection AddAppDbContext<TDbContext>(
        this IServiceCollection services, 
        string connString, 
        IModel? model = null)
        where TDbContext : DbContext, IDatabaseFacade, IDomainEventContext

    {
        services.AddDbContextPool<TDbContext>((sp, options) =>
        {
            options.UseNpgsql(connString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                        null);
                })
                .UseExceptionProcessor()
                .UseSnakeCaseNamingConvention()
                .EnableServiceProviderCaching()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (model is not null)
                options.UseModel(model);

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
}