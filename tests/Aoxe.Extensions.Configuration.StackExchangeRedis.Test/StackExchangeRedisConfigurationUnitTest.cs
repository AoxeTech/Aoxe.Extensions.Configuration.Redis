namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Test;

public class RedisConfigurationUnitTest
{
    [Fact]
    public async Task ConfigurationTest()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"configuration-test-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        const string initValue = "SettingValue";
        await db.StringSetAsync(redisKey, initValue);

        var configBuilder = new ConfigurationBuilder().AddRedis(
            new RedisClientOptions("localhost:6379"),
            redisKey
        );
        var configuration = configBuilder.Build();

        // Act
        var settingValue = configuration[redisKey];

        Assert.Equal(initValue, settingValue);
    }

    [Fact]
    public async Task Test_ReloadFeature()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"reload-test-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        const string initialSettings = "InitialValue";
        await db.StringSetAsync(redisKey, initialSettings);

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddRedis(
            new RedisClientOptions("localhost:6379"),
            redisKey,
            null,
            true
        );
        var configuration = configurationBuilder.Build();

        // Act
        var initialValue = configuration[redisKey];

        // Update the configuration in Redis
        const string updatedSettings = "UpdatedValue";
        await db.StringSetAsync(redisKey, updatedSettings);

        // Poll for the updated value
        string? updatedValue = null;
        var timeout = TimeSpan.FromSeconds(10);
        var startTime = DateTime.UtcNow;

        while (DateTime.UtcNow - startTime < timeout)
        {
            updatedValue = configuration[redisKey];
            if (updatedValue != initialValue)
            {
                break; // Exit loop if the value has changed
            }
            await Task.Delay(100); // Wait before checking again
        }

        // Assert
        Assert.NotEqual(initialValue, updatedValue);
        Assert.Equal("UpdatedValue", updatedValue);
    }
}
