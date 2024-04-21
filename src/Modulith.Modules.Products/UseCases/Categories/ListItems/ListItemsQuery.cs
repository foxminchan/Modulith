using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.ListItems;

public sealed record ListItemsQuery : IQuery<Result<List<CategoryVm>>>;