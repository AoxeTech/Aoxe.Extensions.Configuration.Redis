namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisConfigurationProvider(
    RedisConfigurationSource source,
    IRedisDatabaseFactory redisDatabaseFactory,
    IFlattener? flattener = null
) : ConfigurationProvider
{
    public override void Load()
    {
        var db = redisDatabaseFactory.Create();
        var value = db.StringGet(source.Key);
        Data = flattener is null
            ? new Dictionary<string, string?> { { source.Key, value } }
            : flattener.Flatten(new MemoryStream(value!));
    }
}
