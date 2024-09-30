namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationSource(
    RedisClientOptions redisClientOptions,
    string key,
    IFlattener? flattener = null,
    bool reloadOnChange = false
) : IConfigurationSource
{
    public RedisClientOptions RedisClientOptions { get; } = redisClientOptions;
    public string Key { get; } = key;
    public bool ReloadOnChange { get; } = reloadOnChange;

    public RedisConfigurationSource(
        Func<RedisClientOptions> optionsFactory,
        string key,
        IFlattener? flattener = null,
        bool reloadOnChange = false
    )
        : this(optionsFactory(), key, flattener, reloadOnChange) { }

    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new RedisConfigurationProvider(this, flattener);
}
