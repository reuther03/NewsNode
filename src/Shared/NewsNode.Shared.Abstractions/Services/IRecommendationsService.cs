using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Services;

public interface IRecommendationsService
{
    Task CreateRecommendation(UserId userId, List<Hashtag> hashtags, CancellationToken cancellationToken = default);
    Task IncrementRecommendation(UserId userId, Hashtag hashtag, CancellationToken cancellationToken = default);
}