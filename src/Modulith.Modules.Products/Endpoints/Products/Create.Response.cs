namespace Modulith.Modules.Products.Endpoints.Products;

public sealed class CreateProductResponse(Guid id)
{
    public Guid Id { get; set; } = id;
}