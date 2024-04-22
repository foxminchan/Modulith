using Modulith.Modules.Baskets.Domain;

namespace Modulith.Modules.Baskets.ViewModels;

public sealed record BasketItemVm(
    Guid ProductId,
    string? ProductName,
    int Quantity,
    decimal Price,
    decimal Total
)
{
    public static BasketItemVm FromEntity(BasketItem item) =>
        new(
            item.Id,
            item.ProductName,
            item.Quantity,
            item.Price,
            item.Quantity * item.Price
        );
}