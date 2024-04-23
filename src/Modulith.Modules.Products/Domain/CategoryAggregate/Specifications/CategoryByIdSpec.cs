using Ardalis.Specification;

namespace Modulith.Modules.Products.Domain.CategoryAggregate.Specifications;

public sealed class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .EnableCache(nameof(CategoryByIdSpec), id);
}