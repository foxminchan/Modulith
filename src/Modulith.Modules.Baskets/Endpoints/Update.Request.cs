using Modulith.Modules.Baskets.Domain;

namespace Modulith.Modules.Baskets.Endpoints;

public sealed class UpdateBasketRequest
{
    public  Guid CustomerId { get; set; }
    public BasketItem Item { get; set; } = default!;
}