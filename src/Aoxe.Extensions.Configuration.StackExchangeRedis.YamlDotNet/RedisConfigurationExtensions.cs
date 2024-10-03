namespace Aoxe.Extensions.Configuration.StackExchangeRedis.YamlDotNet;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisYaml(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(
            new RedisConfigurationSource(optionsFactory, key, new YamlFlattener(), reloadOnChange)
        );

    public static IConfigurationBuilder AddRedisYaml(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(
            new RedisConfigurationSource(options, key, new YamlFlattener(), reloadOnChange)
        );
}
