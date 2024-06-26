﻿using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules.Products.Infrastructures.Data.CompiledModels;
using Modulith.Persistence;

namespace Modulith.Modules.Products.Infrastructures.Data;

public static class Extension
{
    public static void AddProductDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("ProductDb");
        Guard.Against.NullOrEmpty(connString);
        services.AddAppDbContext<ProductDbContext>(
            connString,
            ProductDbContextModel.Instance,
            svc => svc.AddRepository(typeof(ProductRepository<>))
        ).AddDatabaseDeveloperPageExceptionFilter();
    }
}