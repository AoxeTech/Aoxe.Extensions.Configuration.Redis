namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public class RedisClientOptions
{
    public ConfigurationOptions Options { get; set; }
    public TextWriter? Log { get; set; }

    public RedisClientOptions(Func<ConfigurationOptions> optionsFactory) =>
        Options = optionsFactory();

    public RedisClientOptions(ConfigurationOptions options) => Options = options;

    public RedisClientOptions(string configuration) =>
        Options = ConfigurationOptions.Parse(configuration);
}
