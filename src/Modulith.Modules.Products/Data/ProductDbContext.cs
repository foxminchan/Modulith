using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Persistence;
using Modulith.Persistence.Constants;
using SmartEnum.EFCore;

namespace Modulith.Modules.Products.Data;

public sealed class ProductDbContext(DbContextOptions options) : ApplicationDbContextBase(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureSmartEnum();
        modelBuilder.HasPostgresExtension(UniqueId.UUID_EXTENSION);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}