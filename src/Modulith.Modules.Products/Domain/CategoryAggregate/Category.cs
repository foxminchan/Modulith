using Ardalis.GuardClauses;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.SharedKernel.Entities;

namespace Modulith.Modules.Products.Domain.CategoryAggregate;

public sealed class Category : EntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Category()
    {
    }

    public Category(string title, string? description)
    {
        Name = Guard.Against.NullOrEmpty(title);
        Description = description;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; } = [];

    public void Update(string title, string? description)
    {
        Name = Guard.Against.NullOrEmpty(title);
        Description = description;
    }
}