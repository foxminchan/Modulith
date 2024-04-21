using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.Endpoints.Categories;

public sealed class DeleteCategoryRequest(CategoryId id)
{
    public CategoryId Id { get; set; } = id;
}