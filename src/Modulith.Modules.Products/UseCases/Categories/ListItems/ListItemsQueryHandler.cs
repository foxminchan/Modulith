using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Repositories;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.ListItems;

public sealed class ListItemsQueryHandler(IReadRepository<Category> repository)
    : IQueryHandler<ListItemsQuery, Result<List<CategoryVm>>>
{
    public async Task<Result<List<CategoryVm>>> Handle(ListItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.ListAsync(cancellationToken);
        var response = result.Select(x => new CategoryVm(x.Id, x.Name, x.Description)).ToList();
        return Result<List<CategoryVm>>.Success(response);
    }
}