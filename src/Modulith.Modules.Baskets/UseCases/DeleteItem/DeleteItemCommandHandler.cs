using Ardalis.GuardClauses;
using Ardalis.Result;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Modules.Baskets.Domain;
using Modulith.SharedKernel.Shared;

namespace Modulith.Modules.Baskets.UseCases.DeleteItem;

public sealed  class DeleteItemCommandHandler(IRedisService redisService)
    : ICommandHandler<DeleteItemCommand, Result>
{
    public Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.Id}:cart";
        var basket = redisService.Get<CustomerBasket>(key);
        Guard.Against.NotFound(key, basket);

        var basketItems = basket.Items.ToDictionary(x => x.Id, x => x.Quantity);
        redisService.Remove(key);
        basket.RemoveItems(basketItems);

        return Task.FromResult(Result.Success());
    }
}