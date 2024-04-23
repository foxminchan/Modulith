namespace Modulith.Modules.Baskets.Domain;

public sealed class BasketItem(Guid productId, string? productName, int quantity, decimal price)
{
    public Guid Id { get; set; } = productId;
    public string? ProductName { get; set; } = productName;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;

    public void Update(Guid productId, string? productName, int quantity, decimal price)
    {
        Id = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }
}