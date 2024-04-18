namespace Modulith.Infrastructure.Cache.Redis;

public sealed class RedisSettings
{
    public bool AbortOnConnectFail { get; set; }
    public bool Ssl { get; set; } = true;
    public byte ConnectRetry { get; set; } = 5;
    public int ConnectTimeout { get; set; } = 5000;
    public int DeltaBackOff { get; set; } = 1000;
    public int RedisDefaultSlidingExpirationInSecond { get; set; } = 3600;
    public int SyncTimeout { get; set; } = 5000;
    public string Password { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public string GetConnectionString()
        => string.IsNullOrEmpty(Password)
            ? Url
            : $"{Url},password={Password},abortConnect=False";
}