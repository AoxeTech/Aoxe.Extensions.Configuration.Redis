namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConnectionFactory(RedisConfigurationSource source)
    : IRedisConnectionFactory,
        IDisposable
{
    private static readonly ConcurrentDictionary<
        RedisClientOptions,
        ConnectionMultiplexer
    > ConnectionMultiplexers = new();

    private static readonly ConcurrentDictionary<string, Task> ConnectionMultiplexerTasks = new();

    public IConnectionMultiplexer GetConnection() =>
        ConnectionMultiplexers.GetOrAdd(
            source.RedisClientOptions,
            redisClientOptions =>
                ConnectionMultiplexer.Connect(redisClientOptions.Options, redisClientOptions.Log)
        );

    public void Dispose()
    {
        foreach (var keyValuePair in ConnectionMultiplexers)
            keyValuePair.Value.Dispose();
    }
}
