namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class GetCategoryRequest(Guid id)
{
    public Guid Id { get; set; } = id;
}