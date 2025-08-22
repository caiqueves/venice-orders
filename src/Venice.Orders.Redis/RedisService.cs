using StackExchange.Redis;
using Microsoft.Extensions.Options;
using Venice.Orders.CrossCutting.Shareable.Config;
using Venice.Orders.Application.Interfaces;


namespace Venice.Orders.Infrastructure.Caching.Redis;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _connection;

    public RedisService(IOptions<AppConfig> opt)
    {
        _connection = ConnectionMultiplexer.Connect(opt.Value.RedisSettings.Connection);
    }

    public IDatabase GetDatabase() => _connection.GetDatabase();

    public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
    {
        var db = GetDatabase();
        await db.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetAsync(string key)
    {
        var db = GetDatabase();
        return await db.StringGetAsync(key);
    }
}

