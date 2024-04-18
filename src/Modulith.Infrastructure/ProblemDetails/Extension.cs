using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Infrastructure.ProblemDetails;

public static class Extension
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
        => services
            .AddProblemDetails()
            .AddSingleton<IDeveloperPageExceptionFilter, DeveloperPageExceptionFilter>();

    public static IApplicationBuilder UseCustomProblemDetails(this IApplicationBuilder app)
        => app.UseDeveloperExceptionPage().UseStatusCodePages();
}