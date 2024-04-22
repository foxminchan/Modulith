using Ardalis.Result;
using Medallion.Threading;
using Microsoft.Extensions.Logging;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Modules.Baskets.Domain;
using Modulith.SharedKernel.Shared;
using System.Text.Json;

namespace Modulith.Modules.Baskets.UseCases.AddItem;

public sealed class AddItemCommandHandler(
    IRedisService redisService,
    ILogger<AddItemCommandHandler> logger,
    IDistributedLockProvider distributedLockProvider) : ICommandHandler<AddItemCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        BasketItem basketItem = new(
            request.Item.Id,
            request.Item.ProductName,
            request.Item.Quantity,
            request.Item.Price
        );

        var key = $"user:{request.CustomerId}:cart";
        var basket = redisService.Get<CustomerBasket>(key) ?? new CustomerBasket { Id = request.CustomerId };

        basket.Items.Add(basketItem);

        logger.LogInformation("[{Command}] Basket information: {Basket}", nameof(AddItemCommand),
            JsonSerializer.Serialize(basket));

        await using (await distributedLockProvider.TryAcquireLockAsync(key, cancellationToken: cancellationToken))
        {
            logger.LogInformation("[{Command}] Lock acquired for key: {Key}", nameof(AddItemCommand), key);
            redisService.HashGetOrSet(key, request.CustomerId.ToString(), () => basket);
            basket.AddItem(basketItem);
        }

        return Result<Guid>.Success(basket.Id);
    }
}