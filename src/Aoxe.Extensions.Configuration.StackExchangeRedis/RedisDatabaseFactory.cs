namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisDatabaseFactory(RedisConfigurationSource source) : IRedisDatabaseFactory
{
    public IDatabase Create() =>
        ConnectionMultiplexer
            .Connect(source.RedisClientOptions.Options, source.RedisClientOptions.Log)
            .GetDatabase();
}
