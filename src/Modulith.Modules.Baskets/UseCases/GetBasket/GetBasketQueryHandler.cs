using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Modules.Baskets.Domain;
using Modulith.Modules.Baskets.ViewModels;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.GetBasket;

public sealed class GetBasketQueryHandler(IRedisService redisService)
    : IQueryHandler<GetBasketQuery, Result<CustomerBasketVm>>
{
    public Task<Result<CustomerBasketVm>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.BasketId}:cart";
        var basket = redisService.HashGetOrSet<CustomerBasket>(key, request.BasketId.ToString(), () => new());
        Guard.Against.NotFound(key, basket);
        var result = Result<CustomerBasketVm>.Success(CustomerBasketVm.FromEntity(basket));
        return Task.FromResult(result);
    }
}