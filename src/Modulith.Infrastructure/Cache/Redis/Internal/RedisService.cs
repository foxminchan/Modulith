using System.Text;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Modulith.Infrastructure.Cache.Redis.Internal;

public sealed class RedisService(IOptions<RedisSettings> options) : IRedisService
{
    private const string GET_KEYS_LUA_SCRIPT = """
                                                   local pattern = ARGV[1]
                                                   local keys = redis.call('KEYS', pattern)
                                                   return keys
                                               """;

    private const string CLEAR_CACHE_LUA_SCRIPT = """
                                                      local pattern = ARGV[1]
                                                      for _,k in ipairs(redis.call('KEYS', pattern)) do
                                                          redis.call('DEL', k)
                                                      end
                                                  """;

    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer = new(
        () => ConnectionMultiplexer.Connect(options.Value.GetConnectionString())
    );

    private readonly RedisSettings _redisCacheSetting = options.Value;

    private ConnectionMultiplexer ConnectionMultiplexer => _connectionMultiplexer.Value;

    private IDatabase Database
    {
        get
        {
            _connectionLock.Wait();

            try
            {
                return ConnectionMultiplexer.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }

    public T GetOrSet<T>(string key, Func<T> valueFactory)
        => GetOrSet($"{_redisCacheSetting.Prefix}:{key}", valueFactory,
            TimeSpan.FromSeconds(_redisCacheSetting.RedisDefaultSlidingExpirationInSecond));

    public T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration)
    {
        var keyWithPrefix = $"{_redisCacheSetting.Prefix}:{key}";

        Guard.Against.NullOrEmpty(key);

        var cachedValue = Database.StringGet(keyWithPrefix);
        if (!string.IsNullOrEmpty(cachedValue)) return GetByteToObject<T>(cachedValue);

        var newValue = valueFactory();
        if (newValue is not null) Database.StringSet(keyWithPrefix, JsonSerializer.Serialize(newValue), expiration);

        return newValue;
    }

    public T? Get<T>(string key)
    {
        var keyWithPrefix = $"{_redisCacheSetting.Prefix}:{key}";

        Guard.Against.NullOrEmpty(_redisCacheSetting.Prefix);

        var cachedValue = Database.StringGet(keyWithPrefix);
        return !string.IsNullOrEmpty(cachedValue)
            ? GetByteToObject<T>(cachedValue)
            : default;
    }

    public T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory)
    {
        Guard.Against.NullOrEmpty(key);
        Guard.Against.NullOrEmpty(hashKey);

        var keyWithPrefix = $"{_redisCacheSetting.Prefix}:{key}";
        var value = Database.HashGet(keyWithPrefix, hashKey.ToLower());

        if (!string.IsNullOrEmpty(value)) return GetByteToObject<T>(value);

        if (valueFactory() is not null)
            Database.HashSet(keyWithPrefix, hashKey.ToLower(),
                JsonSerializer.Serialize(valueFactory()));

        return valueFactory();
    }

    public IEnumerable<string> GetKeys(string pattern)
        => ((RedisResult[])Database.ScriptEvaluate(GET_KEYS_LUA_SCRIPT, values: [pattern])!)
            .Where(x => x.ToString().StartsWith(_redisCacheSetting.Prefix))
            .Select(x => x.ToString())
            .ToArray();

    public IEnumerable<T> GetValues<T>(string key)
        => Database.HashGetAll($"{_redisCacheSetting.Prefix}:{key}").Select(x => GetByteToObject<T>(x.Value));

    public bool RemoveAllKeys(string pattern = "*")
    {
        var succeed = true;

        var keys = GetKeys($"{_redisCacheSetting.Prefix}:{pattern}");
        foreach (var key in keys) succeed = Database.KeyDelete(key);

        return succeed;
    }

    public void Remove(string key) => Database.KeyDelete($"{_redisCacheSetting.Prefix}:{key}");

    public void Reset()
        => Database.ScriptEvaluate(
            CLEAR_CACHE_LUA_SCRIPT,
            values: [_redisCacheSetting.Prefix + "*"],
            flags: CommandFlags.FireAndForget);

    private static T GetByteToObject<T>(RedisValue value)
    {
        var result = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value!));
        return result is null ? throw new InvalidOperationException("Deserialization failed.") : result;
    }
}