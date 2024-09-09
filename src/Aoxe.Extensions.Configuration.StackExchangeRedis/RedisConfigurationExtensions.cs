namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedis(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key,
        IFlattener? flattener = null
    ) => builder.Add(new RedisConfigurationSource(optionsFactory, key, flattener));

    public static IConfigurationBuilder AddRedis(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key,
        IFlattener? flattener = null
    ) => builder.Add(new RedisConfigurationSource(options, key, flattener));
}
