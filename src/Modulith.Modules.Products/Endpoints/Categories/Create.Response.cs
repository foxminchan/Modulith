namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class CreateCategoryResponse(Guid id)
{
    public Guid Id { get; set; } = id;
}