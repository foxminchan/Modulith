using Microsoft.AspNetCore.Http;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed record CreateProductRequest(
    string ProductName,
    string? ProductCode,
    string? Detail,
    int Quantity,
    Guid? CategoryId,
    decimal Price,
    decimal PriceSale,
    IFormFile? Image,
    string? Alt
);