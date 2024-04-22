using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules.Products.Infrastructures.Data;
using Modulith.Modules.Products.Infrastructures.Storage;

namespace Modulith.Modules.Products;

public static class Extension
{
    public static IServiceCollection AddProductModule(this WebApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.AddAzureStorage();
        builder.Services.AddProductDbContext(builder.Configuration);
        assemblies.Add(AssemblyReference.Assembly);
        return builder.Services;
    }
}