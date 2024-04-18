using MediatR;

namespace Modulith.Domain.Events;

public sealed class EventWrapper(DomainEventBase @event) : INotification
{
    public DomainEventBase Event { get; } = @event;
}