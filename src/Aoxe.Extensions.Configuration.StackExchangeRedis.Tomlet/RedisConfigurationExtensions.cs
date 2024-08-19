namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Tomlet;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddEtcdToml(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new TomlFlattener()));
}
