using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules.Orders.Infrastructures.Data.CompiledModels;
using Modulith.Persistence;

namespace Modulith.Modules.Orders.Infrastructures.Data;

public static class Extension
{
    public static void AddOrderDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("OrderDb");
        Guard.Against.NullOrEmpty(connString);
        services.AddAppDbContext<OrderDbContext>(
            connString,
            OrderDbContextModel.Instance,
            svc => svc.AddRepository(typeof(OrderRepository<>))
        ).AddDatabaseDeveloperPageExceptionFilter();
    }
}