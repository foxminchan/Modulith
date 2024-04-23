using Ardalis.Result;
using Modulith.Modules.Products.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.UpdateItem;

public sealed record UpdateItemCommand(Guid Id, string Name, string? Description)
    : ICommand<Result<CategoryVm>>;