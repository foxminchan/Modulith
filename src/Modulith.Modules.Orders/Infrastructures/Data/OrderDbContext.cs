using Microsoft.EntityFrameworkCore;
using Modulith.Persistence;
using SmartEnum.EFCore;
using System.Reflection;
using Modulith.Persistence.Constants;

namespace Modulith.Modules.Orders.Infrastructures.Data;

public sealed class OrderDbContext(DbContextOptions options) : ApplicationDbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureSmartEnum();
        modelBuilder.HasPostgresExtension(UniqueId.UUID_EXTENSION);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}