using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modulith.Infrastructure.Cache.Redis;
using Modulith.Infrastructure.Cache.Redis.Internal;
using StackExchange.Redis;

namespace Modulith.Infrastructure.Cache;

public static class Extension
{
    public static IServiceCollection AddRedisCache(
        this IHostApplicationBuilder builder,
        Action<RedisSettings>? setupAction = null)
    {
        if (builder.Services.Contains(ServiceDescriptor.Singleton<IRedisService, RedisService>()))
            return builder.Services;

        RedisSettings redisSettings = new();
        var redisCacheSection = builder.Configuration.GetSection(nameof(RedisSettings));
        redisCacheSection.Bind(redisSettings);
        builder.Services.Configure<RedisSettings>(redisCacheSection);
        setupAction?.Invoke(redisSettings);

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = builder.Configuration[redisSettings.Prefix];
            options.ConfigurationOptions = GetRedisConfigurationOptions(redisSettings, builder.Configuration);
        });

        builder.Services.AddSingleton<IRedisService, RedisService>();

        return builder.Services;
    }

    private static ConfigurationOptions GetRedisConfigurationOptions(RedisSettings redisSettings, IConfiguration config)
    {
        ConfigurationOptions configurationOptions = new()
        {
            ConnectTimeout = redisSettings.ConnectTimeout,
            SyncTimeout = redisSettings.SyncTimeout,
            ConnectRetry = redisSettings.ConnectRetry,
            AbortOnConnectFail = redisSettings.AbortOnConnectFail,
            ReconnectRetryPolicy = new ExponentialRetry(redisSettings.DeltaBackOff),
            KeepAlive = 5,
            Ssl = redisSettings.Ssl
        };

        if (!string.IsNullOrWhiteSpace(redisSettings.Password)) configurationOptions.Password = redisSettings.Password;

        redisSettings.Url = config.GetSection(nameof(RedisSettings)).Get<RedisSettings>()?.Url
                            ?? throw new InvalidOperationException();

        foreach (var endpoint in redisSettings.Url.Split(',')) configurationOptions.EndPoints.Add(endpoint);

        return configurationOptions;
    }
}