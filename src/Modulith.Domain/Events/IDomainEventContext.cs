namespace Modulith.Domain.Events;

public interface IDomainEventContext
{
    IEnumerable<DomainEventBase> GetDomainEvents();
}