using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Services;

public interface IRecommendationsService
{
    Task CreateActionRecommendation(UserId userId, List<Hashtag> hashtags, CancellationToken cancellationToken = default);
    Task IncrementRecommendation(UserId userId, List<Hashtag> hashtags, PostActionType postActionType, CancellationToken cancellationToken = default);

    Task<List<Hashtag>> GetRecommendedHashtags(UserId userId, CancellationToken cancellationToken = default);
}