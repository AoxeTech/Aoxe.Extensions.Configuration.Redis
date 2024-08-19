namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Tomlyn.Test;

public class RedisConfigurationUnitTest
{
    [Fact]
    public void ConfigurationTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedis(
            new RedisClientOptions("localhost:6379"),
            "test-toml",
            new TomlFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public void ConfigurationIniTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedisToml(
            new RedisClientOptions("localhost:6379"),
            "test-toml"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
