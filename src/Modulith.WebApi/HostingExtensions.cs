using Microsoft.Extensions.DependencyInjection.Extensions;
using Modulith.Infrastructure;
using Modulith.Infrastructure.Endpoint;
using Modulith.Modules.Products;
using System.Reflection;
using MediatR;
using Modulith.Infrastructure.Logging;
using Modulith.Infrastructure.Validator;
using Modulith.Modules.Baskets;
using Modulith.Modules.Orders;
using Modulith.Persistence;

namespace Modulith.WebApi;

public static class HostingExtensions
{
    public static void AddInfrastructureService(this IServiceCollection services, WebApplicationBuilder builder)
        => services.AddInfrastructure(builder);

    public static void UseInfrastructureService(this WebApplication app) => app.UseInfrastructure();

    public static void AddModuleServices(this WebApplicationBuilder builder)
    {
        List<Assembly> assemblies = [AssemblyReference.Program];
        builder.AddProductModule(assemblies);
        builder.AddBasketModule(assemblies);
        builder.AddOrderModule(assemblies);
        builder.AddMediator(assemblies);
        builder.AddEndpoints(assemblies);
    }

    private static void AddMediator(this IHostApplicationBuilder builder, List<Assembly> assemblies) =>
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies([.. assemblies]);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>),
                ServiceLifetime.Scoped);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>),
                ServiceLifetime.Scoped);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>),
                ServiceLifetime.Scoped);
        });

    private static void AddEndpoints(this IHostApplicationBuilder builder, IEnumerable<Assembly> assemblies)
    {
        foreach (var serviceDescriptors in assemblies.Select(assembly => assembly
                     .DefinedTypes
                     .Where(type => type.GetInterfaces().Contains(typeof(IEndpointBase)))
                     .Where(type => !type.IsInterface)
                     .Select(type => ServiceDescriptor.Scoped(typeof(IEndpointBase), type))
                     .ToArray()))
        {
            builder.Services.TryAddEnumerable(serviceDescriptors);
        }
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var endpoints = scope.ServiceProvider.GetRequiredService<IEnumerable<IEndpointBase>>();

        var apiVersionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new(1, 0))
            .HasApiVersion(new(2, 0))
            .ReportApiVersions()
            .Build();

        IEndpointRouteBuilder builder = app
            .MapGroup("/api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        foreach (var endpoint in endpoints) endpoint.MapEndpoint(builder);

        return app;
    }

    public static IApplicationBuilder MapSpecialEndpoints(this WebApplication app)
    {
        app.Map("/", () => Results.Redirect("/swagger"));

        app.Map("/error",
            () => Results.Problem(
                "An unexpected error occurred.",
                statusCode: StatusCodes.Status500InternalServerError
            )).ExcludeFromDescription();

        app.MapPrometheusScrapingEndpoint();

        return app;
    }

    public static IServiceCollection AddCustomCors(this IHostApplicationBuilder builder, string corsName = "api")
    {
        builder.Services.AddCors(options => options.AddPolicy(corsName,
            policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));

        return builder.Services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app, string corsName = "api")
        => app.UseCors(corsName);
}