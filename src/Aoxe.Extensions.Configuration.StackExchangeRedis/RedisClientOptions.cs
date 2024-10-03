namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public record RedisClientOptions
{
    public ConfigurationOptions Options { get; set; }
    public int Db { get; set; }
    public TextWriter? Log { get; set; }

    public RedisClientOptions(
        Func<ConfigurationOptions> optionsFactory,
        int db = 0,
        TextWriter? log = null
    ) => (Options, Db, Log) = (optionsFactory(), db, log);

    public RedisClientOptions(ConfigurationOptions options, int db = 0, TextWriter? log = null) =>
        (Options, Db, Log) = (options, db, log);

    public RedisClientOptions(string configuration, int db = 0, TextWriter? log = null) =>
        (Options, Db, Log) = (ConfigurationOptions.Parse(configuration), db, log);
}
