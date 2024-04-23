using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Products.UseCases.Products.DeleteItem;

public sealed record DeleteItemCommand(Guid Id, bool IsRemoveImage) : ICommand<Result>;