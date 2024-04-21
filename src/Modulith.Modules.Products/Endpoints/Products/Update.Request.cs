using Microsoft.AspNetCore.Http;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed record UpdateProductRequest(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    decimal Price,
    decimal PriceSale,
    bool IsDeleteImage,
    IFormFile? Image,
    string? Alt
);