using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Modulith.SharedKernel.Entities;
using Modulith.SharedKernel.Events;

namespace Modulith.Persistence;

public abstract class ApplicationDbContextBase(DbContextOptions options)
    : DbContext(options), IDomainEventContext, IDatabaseFacade
{
    public IEnumerable<DomainEventBase> GetDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToImmutableList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToImmutableList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        return domainEvents;
    }
}