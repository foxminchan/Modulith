namespace Modulith.Modules.Baskets.Endpoints;

public sealed record CheckOutBasketRequest(Guid BasketId, string? Code = null);