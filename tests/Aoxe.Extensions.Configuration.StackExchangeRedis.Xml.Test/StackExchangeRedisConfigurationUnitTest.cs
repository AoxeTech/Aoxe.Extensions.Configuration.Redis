namespace Aoxe.Extensions.Configuration.StackExchangeRedis.Xml.Test;

public class RedisConfigurationUnitTest
{
    [Fact]
    public void ConfigurationTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedis(
            new RedisClientOptions("localhost:6379"),
            "test-xml",
            new XmlFlattener()
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }

    [Fact]
    public void ConfigurationXmlTest()
    {
        var configBuilder = new ConfigurationBuilder().AddRedisXml(
            new RedisClientOptions("localhost:6379"),
            "test-xml"
        );
        var configuration = configBuilder.Build();
        Assert.Equal("nestedStringValue", configuration["nestedObject:nestedStringKey"]);
    }
}
