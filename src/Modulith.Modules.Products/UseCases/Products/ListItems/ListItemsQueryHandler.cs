using Ardalis.Result;
using Modulith.Modules.Products.Domain.ProductAggregate;
using Modulith.Modules.Products.Domain.ProductAggregate.Specifications;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.ListItems;

public sealed class ListItemsQueryHandler(IReadRepository<Product> repository)
    : IQueryHandler<ListItemsQuery, PagedResult<List<ProductVm>>>
{
    public async Task<PagedResult<List<ProductVm>>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
    {
        ProductsFilterSpec spec = new(
            request.PageIndex,
            request.PageSize,
            request.IsAscending,
            request.OrderBy,
            request.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.PageSize);
        PagedInfo pageInfo = new(request.PageIndex, request.PageSize, totalPages, totalRecords);
        var response = entities.Select(ProductVm.FromEntity).ToList();
        return new(pageInfo, response);
    }
}