using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.GetItem;

public sealed record GetItemQuery(Guid Id) : IQuery<Result<CategoryVm>>;