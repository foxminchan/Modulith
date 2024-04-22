using Ardalis.GuardClauses;
using Ardalis.Result;
using Medallion.Threading;
using Microsoft.Extensions.Logging;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Modules.Baskets.Domain;
using Modulith.Modules.Baskets.ViewModels;
using Modulith.SharedKernel.Shared;
using System.Text.Json;

namespace Modulith.Modules.Baskets.UseCases.UpdateItem;

public sealed class UpdateItemCommandHandler(
    IRedisService redisService,
    ILogger<UpdateItemCommandHandler> logger,
    IDistributedLockProvider distributedLockProvider) : ICommandHandler<UpdateItemCommand, Result<CustomerBasketVm>>
{
    public async Task<Result<CustomerBasketVm>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        BasketItem basketItem = new(
            request.Item.Id,
            request.Item.ProductName,
            request.Item.Quantity,
            request.Item.Price
        );

        var basket = redisService.Get<CustomerBasket>(key);
        Guard.Against.NotFound(key, basket);

        var item = basket.Items.Find(x => x.Id == basketItem.Id);
        Guard.Against.NotFound(nameof(basketItem.Id), item);

        var isReduce = basketItem.Quantity < item.Quantity;

        if (item.Quantity == 0)
            basket.Items.Remove(item);
        else
            basket.Items[basket.Items.IndexOf(item)].Update(
                basketItem.Id,
                basketItem.ProductName,
                basketItem.Quantity,
                basketItem.Price
            );

        logger.LogInformation("[{Command}] Basket information: {Basket}", nameof(UpdateItemCommand),
            JsonSerializer.Serialize(basket));

        await using (await distributedLockProvider.TryAcquireLockAsync(key, cancellationToken: cancellationToken))
        {
            logger.LogInformation("[{Command}] Lock acquired for key: {Key}", nameof(UpdateItemCommand), key);
            redisService.HashGetOrSet(key, request.CustomerId.ToString(), () => basket);
            basket.UpdateItem(basketItem, isReduce);
        }

        var response = CustomerBasketVm.FromEntity(basket);

        return Result<CustomerBasketVm>.Success(response);
    }
}