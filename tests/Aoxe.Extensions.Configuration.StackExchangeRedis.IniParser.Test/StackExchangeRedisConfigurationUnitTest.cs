namespace Aoxe.Extensions.Configuration.StackExchangeRedis.IniParser.Test;

public class RedisConfigurationUnitTest
{
    private const string InitValue = """
                                     [stringSection]
                                     stringKey = stringValue

                                     [numberSection]
                                     numberKey = 123

                                     [booleanSection]
                                     booleanKey = true

                                     [nullSection]
                                     nullKey =  ; INI files do not have a native null type, so we use an empty value

                                     [nestedSection]
                                     nestedStringKey = nestedStringValue
                                     nestedNumberKey = 456
                                     nestedBooleanKey = false
                                     nestedNullKey =  ; Again, using an empty value for null

                                     [arraySection]
                                     arrayKey1 = arrayStringValue
                                     arrayKey2 = 789
                                     arrayKey3 = false
                                     arrayKey4 =  ; Empty value for null
                                     arrayKey5 = arrayNestedObjectValue
                                     """;

    [Fact]
    public async Task AddRedisIni_WithOptionsFactory_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-ini-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisIni(
            () => new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }

    [Fact]
    public async Task AddRedisIni_WithOptions_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-ini-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisIni(
            new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }
}
