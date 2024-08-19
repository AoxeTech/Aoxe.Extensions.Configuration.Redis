namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationSource(
    RedisClientOptions redisClientOptions,
    string key,
    IFlattener? flattener = null
) : IConfigurationSource
{
    public RedisClientOptions RedisClientOptions { get; } = redisClientOptions;
    public string Key { get; } = key;

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new RedisConfigurationProvider(this, flattener);
}
