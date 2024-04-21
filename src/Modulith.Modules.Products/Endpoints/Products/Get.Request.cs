using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed record GetProductRequest(ProductId Id);