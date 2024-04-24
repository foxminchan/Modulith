using Ardalis.Specification;

namespace Modulith.Modules.Orders.Domain.Specifications;

public sealed class OrderByIdSpec : Specification<Order>
{
    public OrderByIdSpec(Guid id)
        => Query.Where(o => o.Id == id)
            .Include(o => o.OrderItems)
            .EnableCache(nameof(OrderByIdSpec), id);
}