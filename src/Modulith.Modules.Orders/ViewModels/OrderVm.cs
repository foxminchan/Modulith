using Modulith.Modules.Orders.Domain;

namespace Modulith.Modules.Orders.ViewModels;

public sealed record OrderVm(
    Guid Id,
    string? Code,
    List<OrderItemVm> OrderItems,
    decimal Total
)
{
    public static OrderVm FromEntity(Order order) =>
        new(
            order.Id,
            order.Code,
            order.OrderItems.Select(OrderItemVm.FromEntity).ToList(),
            order.OrderItems.Sum(x => x.Price * x.Quantity)
        );
}