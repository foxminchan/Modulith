namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}