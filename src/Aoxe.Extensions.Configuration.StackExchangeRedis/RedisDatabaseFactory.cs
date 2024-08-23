namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisDatabaseFactory(RedisConfigurationSource source) : IRedisDatabaseFactory
{
    public IDatabase Create() =>
        source.RedisClientOptions.Configure is null
            ? ConnectionMultiplexer
                .Connect(source.RedisClientOptions.Configuration, source.RedisClientOptions.Log)
                .GetDatabase()
            : ConnectionMultiplexer
                .Connect(
                    source.RedisClientOptions.Configuration,
                    source.RedisClientOptions.Configure,
                    source.RedisClientOptions.Log
                )
                .GetDatabase();
}
