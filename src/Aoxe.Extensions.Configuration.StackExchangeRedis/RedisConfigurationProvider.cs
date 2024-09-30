namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationProvider : ConfigurationProvider
{
    private readonly RedisConfigurationSource _source;
    private readonly IFlattener? _flattener;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly RedisChannel _channel;

    public RedisConfigurationProvider(RedisConfigurationSource source, IFlattener? flattener = null)
    {
        _source = source;
        _flattener = flattener;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(
            source.RedisClientOptions.Options,
            source.RedisClientOptions.Log
        );
        _channel = new RedisChannel(source.Key, RedisChannel.PatternMode.Auto); // Use the new constructor
        if (_source.ReloadOnChange)
            SubscribeToChanges();
    }

    public sealed override void Load()
    {
        var value = _connectionMultiplexer.GetDatabase().StringGet(_source.Key);
        Data = _flattener is null
            ? new Dictionary<string, string?> { { _source.Key, value } }
            : _flattener.Flatten(new MemoryStream(value!));
    }

    private void SubscribeToChanges()
    {
        var subscriber = _connectionMultiplexer.GetSubscriber();
        subscriber.Subscribe(
            _channel,
            (_, _) =>
            {
                Load(); // Reload configuration on change
                OnReload(); // Notify that the configuration has changed
            }
        );
    }
}
