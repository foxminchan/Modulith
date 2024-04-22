using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Infrastructure.Cache.Redis;
using StackExchange.Redis;

namespace Modulith.Modules.Baskets.Infrastructure.Lock;

public static class Extension
{
    public static void AddRedisDistributedLock(this IHostApplicationBuilder builder) =>
        builder.Services.AddSingleton<IDistributedLockProvider>(
            _ => new RedisDistributedSynchronizationProvider(
                ConnectionMultiplexer.Connect(
                    builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>()!.Url
                ).GetDatabase()
            ));
}