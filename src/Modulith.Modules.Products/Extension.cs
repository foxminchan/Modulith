using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Modules.Products.Data;

namespace Modulith.Modules.Products;

public static class Extension
{
    public static IServiceCollection AddProductModule(this IHostApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.Services.AddProductDbContext(builder.Configuration);
        assemblies.Add(AssemblyReference.Assembly);
        return builder.Services;
    }
}