namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedis(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key,
        IFlattener? flattener = null
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, flattener));
}
