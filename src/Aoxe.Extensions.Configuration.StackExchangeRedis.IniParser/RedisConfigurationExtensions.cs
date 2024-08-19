namespace Aoxe.Extensions.Configuration.StackExchangeRedis.IniParser;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisIni(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new IniFlattener()));
}
