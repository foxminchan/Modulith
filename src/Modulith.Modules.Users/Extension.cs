using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules.Users.Infrastructure.Data;

namespace Modulith.Modules.Users;

public static class Extension
{
    public static IServiceCollection AddUserModule(this WebApplicationBuilder builder, List<Assembly> assemblies)
    {
        builder.Services.AddUserDbContext(builder.Configuration);
        assemblies.Add(AssemblyReference.Assembly);
        return builder.Services;
    }
}