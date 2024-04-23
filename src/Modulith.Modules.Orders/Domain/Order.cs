using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Orders.Domain;

public sealed class Order : EntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Order()
    {
    }

    public Order(string? code, Guid? customerId)
    {
        Code = code;
        CustomerId = customerId;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Code { get; set; }
    public Guid? CustomerId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    public void UpdateOrder(string? code, Guid? customerId)
    {
        Code = code;
        CustomerId = customerId;
    }

    private void AddOrderItem(OrderItem item) => OrderItems.Add(item);

    public static class Factory
    {
        public static Order Create(
            Guid? id,
            string? code,
            IEnumerable<OrderItem> orderItems)
        {
            Order order = new(code, id);
            foreach (var item in orderItems)
                order.AddOrderItem(item);

            return order;
        }
    }
}