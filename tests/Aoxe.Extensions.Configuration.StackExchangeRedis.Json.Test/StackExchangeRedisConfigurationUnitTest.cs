namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Json.Test;

public class RedisConfigurationUnitTest
{
    private const string InitValue = """
                                   {
                                     "stringKey": "stringValue",
                                     "numberKey": 123,
                                     "booleanKey": true,
                                     "nullKey": null,
                                     "nestedObject": {
                                       "nestedStringKey": "nestedStringValue",
                                       "nestedNumberKey": 456,
                                       "nestedBooleanKey": false,
                                       "nestedNullKey": null
                                     },
                                     "arrayKey": [
                                       "arrayStringValue",
                                       789,
                                       false,
                                       null,
                                       {
                                         "arrayNestedObjectKey": "arrayNestedObjectValue"
                                       }
                                     ]
                                   }
                                   """;

    [Fact]
    public async Task AddRedisIni_WithOptionsFactory_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-json-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisJson(
            () => new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public async Task AddRedisIni_WithOptions_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-json-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisJson(
            new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
