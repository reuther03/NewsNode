using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NewsNode.Shared.Abstractions.Services;
using StackExchange.Redis;

namespace NewsNode.Shared.Infrastructure.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetDataAsync<T>(string key, T data, TimeSpan? expirationTime = null)
    {
        var serializedData = JsonSerializer.Serialize(data);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime
        };

        await _cache.SetStringAsync(key, serializedData, options);
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var serializedData = await _cache.GetStringAsync(key);
        if (serializedData == null)
            return default;

        return JsonSerializer.Deserialize<T>(serializedData);
    }
}