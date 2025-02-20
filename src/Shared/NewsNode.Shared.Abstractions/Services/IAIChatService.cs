using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Shared.Abstractions.Services;

public interface IAIChatService
{
    Task<string> GetRecommendedHashtags(Dictionary<Hashtag, RecommendationWeight> recommendationWeights, CancellationToken cancellationToken = default);
}