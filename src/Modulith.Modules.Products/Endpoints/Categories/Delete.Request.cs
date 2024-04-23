namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class DeleteCategoryRequest(Guid id)
{
    public Guid Id { get; set; } = id;
}