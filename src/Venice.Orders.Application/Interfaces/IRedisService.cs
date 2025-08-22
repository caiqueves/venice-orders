
using Microsoft.Extensions.Caching.Distributed;

namespace Venice.Orders.Application.Interfaces;

public interface IRedisService
{
    Task SetAsync(string key, string value, TimeSpan? expiry = null);

    Task<string?> GetAsync(string key);
}
