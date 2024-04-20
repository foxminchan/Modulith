namespace Modulith.SharedKernel.Events;

public interface IDomainEventContext
{
    IEnumerable<DomainEventBase> GetDomainEvents();
}