using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.GetItem;

public sealed record GetItemQuery(CategoryId Id) : IQuery<Result<CategoryVm>>;