using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;

namespace Modulith.Modules.Products.ViewModels;

public sealed record ProductVm(
    Guid Id,
    string? Name,
    string? ProductCode,
    string? Detail,
    string? Status,
    int Quantity,
    string? Category,
    ProductPrice? Price,
    ProductImage? Image
)
{
    public static ProductVm FromEntity(Product product) =>
        new(
            product.Id,
            product.Name,
            product.ProductCode,
            product.Description,
            product.Status?.Name,
            product.Quantity,
            product.Category?.Name,
            product.Price,
            product.Image
        );
}