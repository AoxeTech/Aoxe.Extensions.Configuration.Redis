namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationProvider : ConfigurationProvider
{
    private readonly RedisConfigurationSource _source;
    private readonly IRedisConnectionFactory _redisConnectionFactory;
    private readonly IFlattener? _flattener;

    public RedisConfigurationProvider(
        RedisConfigurationSource source,
        IRedisConnectionFactory redisConnectionFactory,
        IFlattener? flattener = null
    )
    {
        _source = source;
        _redisConnectionFactory = redisConnectionFactory;
        _flattener = flattener;
        if (!_source.ReloadOnChange)
            return;
        SubscribeToChanges(
            new RedisChannel(
                $"__keyspace@{source.RedisClientOptions.Db}__:{source.Key}",
                RedisChannel.PatternMode.Auto
            )
        );
    }

    public override void Load()
    {
        var value = _redisConnectionFactory
            .GetConnection()
            .GetDatabase(_source.RedisClientOptions.Db)
            .StringGet(_source.Key);
        Data = _flattener is null
            ? new Dictionary<string, string?> { { _source.Key, value } }
            : _flattener.Flatten(new MemoryStream(value!));
    }

    private void SubscribeToChanges(RedisChannel channel)
    {
        var subscriber = _redisConnectionFactory.GetConnection().GetSubscriber();
        subscriber.Subscribe(
            channel,
            (_, _) =>
            {
                Load(); // Reload configuration on change
                OnReload(); // Notify that the configuration has changed
            }
        );
    }
}
