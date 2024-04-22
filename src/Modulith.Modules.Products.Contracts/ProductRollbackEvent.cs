using Modulith.SharedKernel.Events;

namespace Modulith.Modules.Products.Contracts;

public sealed class ProductRollbackEvent(IDictionary<Guid, int> items) : DomainEventBase
{
    public IDictionary<Guid, int> Items { get; set; } = items;
}