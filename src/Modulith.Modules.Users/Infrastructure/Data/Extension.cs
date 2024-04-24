using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules.Users.Infrastructures.Data.CompiledModels;
using Modulith.Persistence;

namespace Modulith.Modules.Users.Infrastructure.Data;

public static class Extension
{
    public static void AddUserDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("UserDb");
        Guard.Against.NullOrEmpty(connString);
        services.AddAppDbContext<UserDbContext>(connString, UserDbContextModel.Instance)
            .AddDatabaseDeveloperPageExceptionFilter();
    }
}