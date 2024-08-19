namespace Aoxe.Extensions.Configuration.StackExchangeRedis.NewtonsoftJson;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisJson(
        this IConfigurationBuilder builder,
        RedisClientOptions redisClientOptions,
        string key
    ) => builder.Add(new RedisConfigurationSource(redisClientOptions, key, new JsonFlattener()));
}
