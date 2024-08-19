namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisClientOptions(string configuration)
{
    public string Configuration { get; set; } = configuration;
    public Action<ConfigurationOptions>? Configure { get; set; }
    public TextWriter? Log { get; set; }
}
