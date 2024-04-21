﻿using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class CreateProductResponse(ProductId id)
{
    public ProductId Id { get; set; } = id;
}