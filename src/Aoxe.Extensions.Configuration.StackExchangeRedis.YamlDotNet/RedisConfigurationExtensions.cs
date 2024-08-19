namespace Aoxe.Extensions.Configuration.StackExchangeRedis.YamlDotNet;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddEtcdYaml(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new YamlFlattener()));
}
