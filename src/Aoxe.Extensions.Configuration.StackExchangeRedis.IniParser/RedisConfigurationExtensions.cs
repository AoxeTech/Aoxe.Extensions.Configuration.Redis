namespace Aoxe.Extensions.Configuration.StackExchangeRedis.IniParser;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisIni(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key
    ) => builder.Add(new RedisConfigurationSource(optionsFactory, key, new IniFlattener()));

    public static IConfigurationBuilder AddRedisIni(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key
    ) => builder.Add(new RedisConfigurationSource(options, key, new IniFlattener()));
}
