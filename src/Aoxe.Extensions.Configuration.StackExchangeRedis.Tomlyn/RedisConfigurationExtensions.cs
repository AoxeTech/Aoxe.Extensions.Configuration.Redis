namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Tomlyn;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisToml(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key
    ) => builder.Add(new RedisConfigurationSource(optionsFactory, key, new TomlFlattener()));

    public static IConfigurationBuilder AddRedisToml(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key
    ) => builder.Add(new RedisConfigurationSource(options, key, new TomlFlattener()));
}
