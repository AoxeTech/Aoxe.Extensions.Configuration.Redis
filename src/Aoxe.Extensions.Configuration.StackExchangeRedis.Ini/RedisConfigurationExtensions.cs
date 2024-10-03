namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Ini;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisIni(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(
            new RedisConfigurationSource(optionsFactory, key, new IniFlattener(), reloadOnChange)
        );

    public static IConfigurationBuilder AddRedisIni(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key,
        bool reloadOnChange = false
    ) =>
        builder.Add(new RedisConfigurationSource(options, key, new IniFlattener(), reloadOnChange));
}
