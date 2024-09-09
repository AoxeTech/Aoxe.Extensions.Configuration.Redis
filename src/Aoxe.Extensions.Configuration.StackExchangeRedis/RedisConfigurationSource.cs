namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationSource(
    RedisClientOptions redisClientOptions,
    string key,
    IFlattener? flattener = null
) : IConfigurationSource
{
    public RedisClientOptions RedisClientOptions { get; } = redisClientOptions;
    public string Key { get; } = key;

    public RedisConfigurationSource(
        Func<RedisClientOptions> optionsFactory,
        string key,
        IFlattener? flattener = null
    )
        : this(optionsFactory(), key, flattener) { }

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new RedisConfigurationProvider(this, new RedisDatabaseFactory(this), flattener);
}
