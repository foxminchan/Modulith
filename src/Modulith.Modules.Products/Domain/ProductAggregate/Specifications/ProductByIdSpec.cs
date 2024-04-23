using Ardalis.Specification;

namespace Modulith.Modules.Products.Domain.ProductAggregate.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(Guid id) =>
        Query.Where(product => product.Id == id)
            .Include(product => product.Category)
            .EnableCache(nameof(ProductByIdSpec), id);
}