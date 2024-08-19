namespace Aoxe.Extensions.Configuration.StackExchangeRedis.IniParser.Test;

public class RedisConfigurationUnitTest
{
    [Fact]
    public void ConfigurationTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedis(
            new RedisClientOptions("localhost:6379"),
            "test-ini",
            new IniFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }

    [Fact]
    public void ConfigurationIniTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedisIni(
            new RedisClientOptions("localhost:6379"),
            "test-ini"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedSection:nestedStringKey"]);
    }
}
