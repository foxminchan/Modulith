using Ardalis.Result;
using Modulith.Modules.Baskets.Domain;
using Modulith.Modules.Baskets.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.UpdateItem;

public sealed record UpdateItemCommand(Guid CustomerId, BasketItem Item)
    : ICommand<Result<CustomerBasketVm>>;