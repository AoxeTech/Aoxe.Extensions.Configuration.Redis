namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationProvider(
    RedisConfigurationSource source,
    IFlattener? flattener = null
) : ConfigurationProvider
{
    private readonly ConnectionMultiplexer _connectionMultiplexer = source
        .RedisClientOptions
        .Configure
        is null
        ? ConnectionMultiplexer.Connect(
            source.RedisClientOptions.Configuration,
            source.RedisClientOptions.Log
        )
        : ConnectionMultiplexer.Connect(
            source.RedisClientOptions.Configuration,
            source.RedisClientOptions.Configure,
            source.RedisClientOptions.Log
        );

    public override void Load()
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = db.StringGet(source.Key);
        Data = flattener is null
            ? new Dictionary<string, string?> { { source.Key, value } }
            : flattener.Flatten(new MemoryStream(value!));
    }
}
