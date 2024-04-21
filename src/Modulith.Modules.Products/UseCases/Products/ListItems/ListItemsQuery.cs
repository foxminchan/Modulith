using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.ListItems;

public sealed record ListItemsQuery(
    string? Search,
    bool IsAscending = true,
    string? OrderBy = "Id",
    int PageIndex = 1,
    int PageSize = 20) : IQuery<PagedResult<List<ProductVm>>>;