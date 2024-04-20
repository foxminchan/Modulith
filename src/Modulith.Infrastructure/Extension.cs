using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Infrastructure.Cache;
using Modulith.Infrastructure.Exception;
using Modulith.Infrastructure.Kestrel;
using Modulith.Infrastructure.ProblemDetails;
using Modulith.Infrastructure.Swagger;
using Modulith.Infrastructure.Validator;
using Modulith.Infrastructure.Versioning;
using System.Diagnostics;
using Modulith.Infrastructure.Storage;
using Modulith.Infrastructure.OpenTelemetry;
using Modulith.Infrastructure.Logging;

namespace Modulith.Infrastructure;

public static class Extension
{
    [DebuggerStepThrough]
    public static void AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddValidator();
        services.AddVersioning();
        services.AddOpenApi();
        services.AddCustomProblemDetails();
        services.AddCustomExceptionHandler();

        builder.AddKestrel();
        builder.AddRedisCache();
        builder.AddAzureStorage();
        builder.AddOpenTelemetry();
        builder.AddSerilog(builder.Environment.ApplicationName);
    }

    [DebuggerStepThrough]
    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseKestrel();
        app.UseOpenApi();
        app.UseCustomProblemDetails();
        app.UseCustomExceptionHandler();
    }
}