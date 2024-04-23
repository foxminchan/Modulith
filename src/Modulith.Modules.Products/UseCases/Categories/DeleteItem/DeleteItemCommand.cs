using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Categories.DeleteItem;

public sealed record DeleteItemCommand(Guid Id) : ICommand<Result>;