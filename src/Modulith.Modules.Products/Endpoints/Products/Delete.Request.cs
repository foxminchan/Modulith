namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class DeleteProductRequest(Guid id, bool isRemoveImage)
{
    public Guid Id { get; set; } = id;
    public bool IsRemoveImage { get; set; } = isRemoveImage;
}