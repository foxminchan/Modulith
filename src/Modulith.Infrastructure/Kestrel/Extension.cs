using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Infrastructure.Kestrel;

public static class Extension
{
    public static void AddKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            options.AllowResponseHeaderCompression = true;
            options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
        });

        builder.Services.AddResponseCompression()
            .AddResponseCaching(options => options.MaximumBodySize = 1024)
            .AddRouting(options => options.LowercaseUrls = true);

        builder.Services.AddOutputCache(
            options => options.AddBasePolicy(policyBuilder => policyBuilder
                .Expire(TimeSpan.FromSeconds(10)).SetVaryByQuery("*")
            ));

        builder.Services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });

        builder.Services.AddRequestTimeouts();
    }

    public static void UseKestrel(this IApplicationBuilder app)
        => app.UseHsts()
            .UseOutputCache()
            .UseRequestTimeouts()
            .UseResponseCompression()
            .UseResponseCaching()
            .UseHttpsRedirection();
}