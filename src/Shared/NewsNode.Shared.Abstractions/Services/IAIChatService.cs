namespace NewsNode.Shared.Abstractions.Services;

public interface IAIChatService
{
    Task<string?> GetRecommendedHashtags(string userId, CancellationToken cancellationToken = default);
}