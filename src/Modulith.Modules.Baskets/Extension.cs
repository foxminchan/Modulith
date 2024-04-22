using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Modules.Baskets.Infrastructure.Lock;
using System.Reflection;

namespace Modulith.Modules.Baskets;

public static class Extension
{
    public static IServiceCollection AddBasketModule(this IHostApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.AddRedisDistributedLock();
        assemblies.Add(AssemblyReference.Assembly);
        return builder.Services;
    }
}