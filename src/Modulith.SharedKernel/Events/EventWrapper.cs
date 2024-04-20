using MediatR;

namespace Modulith.SharedKernel.Events;

public sealed class EventWrapper(DomainEventBase @event) : INotification
{
    public DomainEventBase Event { get; } = @event;
}