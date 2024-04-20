using Ardalis.Specification;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;

namespace Modulith.Modules.Products.Domain.CategoryAggregate.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(CategoryId id) =>
        Query
            .Where(c => c.Id == id)
            .EnableCache(nameof(CategoryId), id);
}