namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Tomlyn.Test;

public class RedisConfigurationUnitTest
{
    private const string InitValue = """
                                     stringKey = "stringValue"
                                     numberKey = 123
                                     booleanKey = true
                                     nullKey = ""  # TOML does not have a native null type, so we use an empty string

                                     [nestedObject]
                                     nestedStringKey = "nestedStringValue"
                                     nestedNumberKey = 456
                                     nestedBooleanKey = false
                                     nestedNullKey = ""  # Again, using an empty string for null

                                     arrayKey = ["arrayStringValue", 789, false, ""]

                                     [[arrayKeyWithNestedObjects]]
                                     arrayNestedObjectKey = "arrayNestedObjectValue"

                                     # Date and time example
                                     dateKey = 1979-05-27T07:32:00Z
                                     """;

    [Fact]
    public async Task AddRedisIni_WithOptionsFactory_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-toml-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisToml(
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
        var redisKey = $"configuration-test-toml-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisToml(
            new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
