using Ardalis.GuardClauses;
using Azure.Identity;
using Azure.Storage.Files.Shares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Infrastructure.Storage.Azure;

namespace Modulith.Infrastructure.HealthCheck;

public static class Extension
{
    public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder builder)
    {
        var redisConn = builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()?.Url;
        Guard.Against.Null(redisConn, message: "Redis URL not found.");

        var azConn = builder.Configuration.GetSection("AzureBlobOption").Get<AzureBlobOption>()?.Url;
        Guard.Against.Null(azConn, message: "AzureBlob URL not found.");

        builder.Services.AddSingleton(_ => new ShareServiceClient(new(azConn), new DefaultAzureCredential()));

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddRedis(redisConn, "Redis", tags: ["redis"])
            .AddAzureFileShare();

        builder.Services
            .AddHealthChecksUI(options =>
            {
                options.AddHealthCheckEndpoint("Health Check API", "/hc");
                options.SetEvaluationTimeInSeconds(60);
                options.DisableDatabaseMigrations();
            })
            .AddInMemoryStorage();

        return builder;
    }

    public static void MapHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/hc",
            new()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });

        app.MapHealthChecks("/liveness", new() { Predicate = r => r.Name.Contains("self") });

        app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
    }
}