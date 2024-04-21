using Ardalis.Specification;
using Modulith.Modules.Products.Domain.ProductAggregate.Primitives;

namespace Modulith.Modules.Products.Domain.ProductAggregate.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(ProductId id) =>
        Query.Where(product => product.Id == id)
            .Include(product => product.Category)
            .EnableCache(nameof(ProductByIdSpec), id);
}