using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Infrastructure.Exception;

public static class Extension
{
    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<ExceptionHandler>();

    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseExceptionHandler();
}