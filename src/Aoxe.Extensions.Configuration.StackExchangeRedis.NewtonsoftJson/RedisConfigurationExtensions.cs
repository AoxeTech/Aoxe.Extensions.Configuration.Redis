﻿namespace Aoxe.Extensions.Configuration.StackExchangeRedis.NewtonsoftJson;

public static class RedisConfigurationExtensions
{
    public static IConfigurationBuilder AddRedisJson(
        this IConfigurationBuilder builder,
        Func<RedisClientOptions> optionsFactory,
        string key
    ) => builder.Add(new RedisConfigurationSource(optionsFactory, key, new JsonFlattener()));

    public static IConfigurationBuilder AddRedisJson(
        this IConfigurationBuilder builder,
        RedisClientOptions options,
        string key
    ) => builder.Add(new RedisConfigurationSource(options, key, new JsonFlattener()));
}
