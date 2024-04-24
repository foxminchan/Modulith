using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.UseCases.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : ICommand<Result>;