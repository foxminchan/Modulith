using Ardalis.Result;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.DeleteItem;

public record DeleteItemCommand(Guid Id) : ICommand<Result>;