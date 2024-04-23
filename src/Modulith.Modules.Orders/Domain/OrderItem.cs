using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Orders.Domain;

public sealed class OrderItem : EntityBase
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public OrderItem()
    {
    }

    public OrderItem(decimal price, int quantity, Guid productId)
    {
        Price = price;
        Quantity = quantity;
        ProductId = productId;
    }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
}