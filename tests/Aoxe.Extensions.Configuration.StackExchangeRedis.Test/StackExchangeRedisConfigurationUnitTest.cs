using System.Text.Json;
using StackExchange.Redis;

namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Test;

public class RedisConfigurationUnitTest
{
    [Fact]
    public void ConfigurationTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedis(
            new RedisClientOptions("localhost:6379"),
            "test-json",
            new JsonFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public async Task Test_ReloadFeature()
    {
        // Arrange
        const string redisConnectionString = "localhost:6379";
        var redisKey = $"reload-test-json-{Guid.NewGuid()}";
        var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
        var db = redis.GetDatabase();

        // Initial configuration
        var initialSettings = new { SomeSetting = "InitialValue" };
        await db.StringSetAsync(redisKey, JsonSerializer.Serialize(initialSettings));

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddRedis(
            new RedisClientOptions("localhost:6379"),
            redisKey,
            new JsonFlattener()
        );
        var configuration = configurationBuilder.Build();

        // Act
        var initialValue = configuration["SomeSetting"];

        // Update the configuration in Redis
        var updatedSettings = new { SomeSetting = "UpdatedValue" };
        await db.StringSetAsync(redisKey, JsonSerializer.Serialize(updatedSettings));

        // Poll for the updated value
        string? updatedValue = null;
        var timeout = TimeSpan.FromSeconds(5);
        var startTime = DateTime.UtcNow;

        while (DateTime.UtcNow - startTime < timeout)
        {
            updatedValue = configuration["SomeSetting"];
            if (updatedValue != initialValue)
            {
                break; // Exit loop if the value has changed
            }
            await Task.Delay(100); // Wait before checking again
        }

        // Assert
        Assert.NotEqual(initialValue, updatedValue);
        Assert.Equal("UpdatedValue", updatedValue);

        await db.KeyDeleteAsync(redisKey);
    }
}
