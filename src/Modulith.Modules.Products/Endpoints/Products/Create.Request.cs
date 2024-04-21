﻿using Microsoft.AspNetCore.Http;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed record CreateProductRequest(
    string ProductName,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    decimal Price,
    decimal PriceSale,
    IFormFile? Image,
    string? Alt
);