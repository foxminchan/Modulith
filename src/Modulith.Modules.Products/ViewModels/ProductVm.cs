using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects;

namespace Modulith.Modules.Products.ViewModels;

public sealed record ProductVm(
    ProductId Id,
    string? Name,
    string? ProductCode,
    string? Detail,
    string? Status,
    int Quantity,
    string? Category,
    ProductPrice? Price,
    ProductImage? Image
);