namespace Aoxe.Extensions.Configuration.StackExchangeRedis;

public interface IRedisConnectionFactory
{
    public IConnectionMultiplexer GetConnection();
}
