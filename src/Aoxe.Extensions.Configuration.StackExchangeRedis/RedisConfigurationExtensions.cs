namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedis(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key,
        IFlattener? flattener = null,
        bool reloadOnChange = false
    ) => builder.Add(new RedisConfigurationSource(optionsFactory, key, flattener, reloadOnChange));

    public static IConfigurationBuilder AddRedis(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key,
        IFlattener? flattener = null,
        bool reloadOnChange = false
    ) => builder.Add(new RedisConfigurationSource(options, key, flattener, reloadOnChange));
}
