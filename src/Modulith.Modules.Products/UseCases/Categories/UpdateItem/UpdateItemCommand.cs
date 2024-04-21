using Ardalis.Result;
using Modulith.Modules.Products.Domain.CategoryAggregate.Primitives;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.UpdateItem;

public sealed record UpdateItemCommand(CategoryId Id, string Name, string? Description)
    : ICommand<Result<CategoryVm>>;