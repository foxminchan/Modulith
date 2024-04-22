using Ardalis.Result;
using Modulith.Modules.Baskets.Domain;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.AddItem;

public sealed record AddItemCommand(Guid CustomerId, BasketItem Item) : ICommand<Result<Guid>>;