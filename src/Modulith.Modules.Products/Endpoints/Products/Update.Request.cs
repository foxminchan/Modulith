using Microsoft.AspNetCore.Http;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed record UpdateProductRequest(
    Guid Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    Guid? CategoryId,
    decimal Price,
    decimal PriceSale,
    bool IsDeleteImage,
    IFormFile? Image,
    string? Alt
);