using Microsoft.Extensions.DependencyInjection.Extensions;
using Modulith.Infrastructure;
using Modulith.Infrastructure.Endpoint;
using Modulith.Modules.Products;

namespace Modulith.WebApi.Extensions;

public static class HostingExtensions
{
    public static void AddInfrastructureService(this IServiceCollection services, WebApplicationBuilder builder)
        => services.AddInfrastructure(builder);

    public static void UseInfrastructureService(this WebApplication app) => app.UseInfrastructure();

    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.AddProductModule();
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

    public static IServiceCollection AddEndpoints(this IHostApplicationBuilder builder)
    {
        var serviceDescriptors = AssemblyReference.Program
            .DefinedTypes
            .Where(type => type.GetInterfaces().Contains(typeof(IEndpointBase)))
            .Where(type => !type.IsInterface)
            .Select(type => ServiceDescriptor.Scoped(typeof(IEndpointBase), type))
            .ToArray();

        builder.Services.TryAddEnumerable(serviceDescriptors);

        return builder.Services;
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
}