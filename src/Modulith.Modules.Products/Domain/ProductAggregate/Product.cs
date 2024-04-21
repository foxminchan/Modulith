using Ardalis.GuardClauses;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.Enums;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Products.Domain.ProductAggregate;

public sealed class Product : SoftDeleteEntity, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Product()
    {
    }

    public Product(
        string name,
        string? productCode,
        string? description,
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice,
        ProductImage? image = null)
    {
        Name = Guard.Against.NullOrEmpty(name);
        ProductCode = productCode;
        Description = description;
        Status = quantity > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = quantity;
        CategoryId = categoryId;
        Price = productPrice;
        Image = image;
    }

    public ProductId Id { get; set; } = new(Guid.NewGuid());
    public string? Name { get; set; }
    public string? ProductCode { get; set; }
    public string? Description { get; set; }
    public ProductStatus? Status { get; set; }
    public int Quantity { get; set; }
    public ProductImage? Image { get; set; }
    public ProductPrice? Price { get; set; }
    public CategoryId? CategoryId { get; set; }
    public Category? Category { get; set; }

    public void Update(
        string name,
        string? productCode,
        string? description,
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice,
        ProductImage? image = null)
    {
        Name = Guard.Against.NullOrEmpty(name);
        ProductCode = productCode;
        Description = description;
        Status = quantity > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = quantity;
        CategoryId = categoryId;
        Price = productPrice;
        Image = image;
    }

    public void RemoveStock(int quantityDesired)
    {
        Quantity -= quantityDesired;
        if (Status == ProductStatus.InStock && Quantity == 0) Status = ProductStatus.OutOfStock;
    }

    public void AddStock(int quantityDesired)
    {
        Quantity += quantityDesired;
        if (Status == ProductStatus.OutOfStock && Quantity > 0) Status = ProductStatus.InStock;
    }

    public void Delete() => IsDeleted = true;
}