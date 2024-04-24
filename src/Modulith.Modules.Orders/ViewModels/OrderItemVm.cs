using Modulith.Modules.Orders.Domain;

namespace Modulith.Modules.Orders.ViewModels;

public sealed record OrderItemVm(
    Guid? ProductId,
    Guid? OrderId,
    int Quantity,
    decimal Price,
    decimal Total
)
{
    public static OrderItemVm FromEntity(OrderItem orderItem) =>
        new(
            orderItem.ProductId,
            orderItem.OrderId,
            orderItem.Quantity,
            orderItem.Price,
            orderItem.Price * orderItem.Quantity
        );
}