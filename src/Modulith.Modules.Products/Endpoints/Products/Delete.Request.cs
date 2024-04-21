using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class DeleteProductRequest(ProductId id, bool isRemoveImage)
{
    public ProductId Id { get; set; } = id;
    public bool IsRemoveImage { get; set; } = isRemoveImage;
}