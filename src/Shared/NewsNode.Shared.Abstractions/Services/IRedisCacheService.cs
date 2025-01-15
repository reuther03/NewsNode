namespace NewsNode.Shared.Abstractions.Services;

public interface IRedisCacheService
{
    Task SetDataAsync<T>(string key, T data, TimeSpan? expirationTime = null);
    Task<T?> GetDataAsync<T>(string key);
}