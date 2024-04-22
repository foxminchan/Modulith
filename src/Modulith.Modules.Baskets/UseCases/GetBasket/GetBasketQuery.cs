using Ardalis.Result;
using Modulith.Modules.Baskets.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.GetBasket;

public sealed record GetBasketQuery(Guid BasketId) : IQuery<Result<CustomerBasketVm>>;