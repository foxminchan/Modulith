using System.ComponentModel.DataAnnotations.Schema;

namespace Modulith.Domain.Events;

public abstract class HasDomainEventsBase
{
    private readonly List<DomainEventBase> _domainEvents = [];

    [NotMapped] public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(DomainEventBase domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}