namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Xml;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisXml(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(
            new RedisConfigurationSource(optionsFactory, key, new XmlFlattener(), reloadOnChange)
        );

    public static IConfigurationBuilder AddRedisXml(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(new RedisConfigurationSource(options, key, new XmlFlattener(), reloadOnChange));
}
