namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Xml;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddEtcdXml(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new XmlFlattener()));
}
