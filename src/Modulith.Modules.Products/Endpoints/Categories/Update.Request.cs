using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class UpdateCategoryRequest
{
    public CategoryId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}