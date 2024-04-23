using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Modulith.Modules.Orders.Infrastructures.Data;

namespace Modulith.Modules.Orders;

public static class Extension
{
    public static IServiceCollection AddOrderModule(this WebApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.Services.AddOrderDbContext(builder.Configuration);
        assemblies.Add(AssemblyReference.Assembly);
        return builder.Services;
    }
}