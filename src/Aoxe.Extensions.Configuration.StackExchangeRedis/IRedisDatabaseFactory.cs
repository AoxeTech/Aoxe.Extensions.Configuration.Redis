namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public interface IRedisDatabaseFactory
{
    IDatabase Create();
}
