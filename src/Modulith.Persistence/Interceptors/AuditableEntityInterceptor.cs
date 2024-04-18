using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Modulith.Domain.Entities;

namespace Modulith.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<EntityBase>())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified) &&
                !entry.References.Any(r =>
                    r.TargetEntry is not null &&
                    r.TargetEntry.Metadata.IsOwned() &&
                    r.TargetEntry.State is EntityState.Added or EntityState.Modified))
                continue;

            if (entry.State == EntityState.Added) entry.Entity.CreatedDate = DateTime.UtcNow;

            entry.Entity.UpdateDate = DateTime.UtcNow;
        }
    }
}