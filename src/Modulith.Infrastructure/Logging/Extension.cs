using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;

namespace Modulith.Infrastructure.Logging;

public static class Extension
{
    public static void AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(
                context.Configuration,
                new ConfigurationReaderOptions { SectionName = sectionName }
            );

            config
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            config.WriteTo.Async(writeTo =>
                writeTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}"));
        });
    }
}