using Modulith.SharedKernel.Events;

namespace Modulith.Modules.Products.Contracts;

public class ProductUpdateQuantityEvent(Guid productId, int quantity, bool isReduce) : DomainEventBase
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
    public bool IsReduce { get; set; } = isReduce;
}