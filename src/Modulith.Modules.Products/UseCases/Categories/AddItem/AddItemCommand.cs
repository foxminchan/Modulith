using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.AddItem;

public sealed record AddItemCommand(string Name, string? Description)
    : ICommand<Result<Guid>>;