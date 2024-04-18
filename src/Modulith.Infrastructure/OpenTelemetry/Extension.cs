using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Modulith.Infrastructure.OpenTelemetry;

public static class Extension
{
    public static void AddOpenTelemetry(this IHostApplicationBuilder builder)
    {
        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService(
                builder.Environment.ApplicationName,
                serviceVersion: "unknown",
                serviceInstanceId: Environment.MachineName
            );

        Uri oltpEndpoint = new(
            builder.Configuration.GetValue<string>("OtlpEndpoint")
            ?? throw new InvalidOperationException("Endpoint is not configured")
        );

        builder.Logging.AddOpenTelemetry(logging =>
            logging.SetResourceBuilder(resourceBuilder)
                .AddOtlpExporter());

        builder.Services.Configure<OpenTelemetryLoggerOptions>(opt =>
        {
            opt.IncludeScopes = true;
            opt.ParseStateValues = true;
            opt.IncludeFormattedMessage = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithTracing(trace =>
                trace.SetResourceBuilder(resourceBuilder)
                    .SetSampler(new AlwaysOnSampler())
                    .AddOtlpExporter(options => options.Endpoint = oltpEndpoint)
                    .AddSource("Microsoft.AspNetCore", "System.Net.Http")
                    .AddEntityFrameworkCoreInstrumentation(b => b.SetDbStatementForText = true)
            )
            .WithMetrics(meter =>
                meter.SetResourceBuilder(resourceBuilder)
                    .AddPrometheusExporter()
                    .AddOtlpExporter(options => options.Endpoint = oltpEndpoint)
                    .AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "System.Net.Http")
                    .AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries =
                                [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                        })
            );
    }
}