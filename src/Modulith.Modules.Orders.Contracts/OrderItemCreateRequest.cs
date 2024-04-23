namespace Modulith.Modules.Orders.Contracts;

public sealed record OrderItemCreateRequest(Guid Id, int Quantity, decimal Price);