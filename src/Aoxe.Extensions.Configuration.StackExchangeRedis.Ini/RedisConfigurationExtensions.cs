namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Ini;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddEtcdIni(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new IniFlattener()));
}
