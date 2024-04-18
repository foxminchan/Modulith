namespace Modulith.Infrastructure.Cache.Redis;

public interface IRedisService
{
    T? Get<T>(string key);

    T GetOrSet<T>(string key, Func<T> valueFactory);

    T GetOrSet<T>(string key, Func<T> valueFactory, TimeSpan expiration);

    T HashGetOrSet<T>(string key, string hashKey, Func<T> valueFactory);

    IEnumerable<string> GetKeys(string pattern);

    IEnumerable<T> GetValues<T>(string key);

    bool RemoveAllKeys(string pattern = "*");

    void Remove(string key);

    void Reset();
}