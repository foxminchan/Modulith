using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modulith.Modules.Users.Domain;
using Modulith.Persistence;
using System.Reflection;
using Modulith.Persistence.Constants;
using Modulith.SharedKernel.Events;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Users.Infrastructure.Data;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options) 
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IDatabaseFacade, IDomainEventContext
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension(UniqueId.UUID_EXTENSION);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}