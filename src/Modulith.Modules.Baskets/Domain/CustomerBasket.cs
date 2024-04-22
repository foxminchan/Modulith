using Modulith.Modules.Products.Contracts;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Baskets.Domain;

public sealed class CustomerBasket : EntityBase, IAggregateRoot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<BasketItem> Items { get; set; } = [];

    public void AddItem(BasketItem basketItem)
        => RegisterDomainEvent(new ProductUpdateQuantityEvent(basketItem.Id, basketItem.Quantity, false));

    public void UpdateItem(BasketItem basketItem, bool isReduce)
        => RegisterDomainEvent(new ProductUpdateQuantityEvent(basketItem.Id, basketItem.Quantity, isReduce));

    public void RemoveItems(Dictionary<Guid, int> basketItems)
        => RegisterDomainEvent(new ProductRollbackEvent(basketItems));
}