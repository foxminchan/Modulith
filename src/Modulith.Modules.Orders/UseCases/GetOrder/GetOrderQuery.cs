using Ardalis.Result;
using Modulith.Modules.Orders.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Orders.UseCases.GetOrder;

public sealed record GetOrderQuery(Guid Id) : IQuery<Result<OrderVm>>;