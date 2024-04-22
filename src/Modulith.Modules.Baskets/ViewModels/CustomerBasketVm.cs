using Modulith.Modules.Baskets.Domain;

namespace Modulith.Modules.Baskets.ViewModels;

public sealed record CustomerBasketVm(
    Guid Id,
    List<BasketItemVm> Items,
    decimal Total
)
{
    public static CustomerBasketVm FromEntity(CustomerBasket basket) =>
        new(
            basket.Id,
            basket.Items.Select(BasketItemVm.FromEntity).ToList(),
            basket.Items.Select(i => i.Quantity * i.Price).Sum()
        );
}