using Modulith.SharedKernel.Events;

namespace Modulith.Modules.Baskets.Contracts;

public sealed class BasketItemDeletedIntegrationEvent(Guid id) : DomainEventBase
{
    public Guid ItemId { get; set; } = id;
}