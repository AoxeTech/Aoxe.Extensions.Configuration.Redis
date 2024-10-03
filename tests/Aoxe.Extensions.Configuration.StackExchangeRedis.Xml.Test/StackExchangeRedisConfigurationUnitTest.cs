namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Xml.Test;

public class RedisConfigurationUnitTest
{
    private const string InitValue = """
                                     <configuration>
                                         <stringKey>stringValue</stringKey>
                                         <numberKey>123</numberKey>
                                         <booleanKey>true</booleanKey>
                                         <nullKey></nullKey> <!-- Using an empty tag for null -->
                                     
                                         <nestedObject>
                                             <nestedStringKey>nestedStringValue</nestedStringKey>
                                             <nestedNumberKey>456</nestedNumberKey>
                                             <nestedBooleanKey>false</nestedBooleanKey>
                                             <nestedNullKey></nestedNullKey> <!-- Using an empty tag for null -->
                                         </nestedObject>
                                     
                                         <arrayKey>
                                             <item>arrayStringValue</item>
                                             <item>789</item>
                                             <item>false</item>
                                             <item></item> <!-- Using an empty tag for null -->
                                         </arrayKey>
                                     
                                         <arrayKeyWithNestedObjects>
                                             <item>
                                                 <arrayNestedObjectKey>arrayNestedObjectValue</arrayNestedObjectKey>
                                             </item>
                                         </arrayKeyWithNestedObjects>
                                     
                                         <dateKey>1979-05-27T07:32:00Z</dateKey>
                                     </configuration>
                                     """;

    [Fact]
    public async Task AddRedisIni_WithOptionsFactory_ShouldLoadConfiguration()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-xml-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisXml(
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
        var redisKey = $"configuration-test-xml-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        await db.StringSetAsync(redisKey, InitValue);

        var configBuilder = new ConfigurationBuilder().AddRedisXml(
            new RedisClientOptions(redisConnectionString),
            redisKey
        );

        // Act
        var configuration = configBuilder.Build();

        // Assert
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
