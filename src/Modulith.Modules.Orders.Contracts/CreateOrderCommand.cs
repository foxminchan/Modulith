using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.Contracts;

public sealed record CreateOrderCommand(
    string? Code,
    Guid? CustomerId,
    List<OrderItemCreateRequest> Items) : ICommand<Result<Guid>>;