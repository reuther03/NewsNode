using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Shared.Abstractions.Services;

public interface IRecommendationsService
{
    Task CreateActionRecommendation(UserId userId, List<Hashtag> hashtags, CancellationToken cancellationToken = default);
    Task IncrementActionRecommendation(UserId userId, List<Hashtag> hashtags, PostActionType postActionType, CancellationToken cancellationToken = default);
    Task<Dictionary<Hashtag, RecommendationWeight>> GetRecommendedHashtags(UserId userId, CancellationToken cancellationToken = default);
    Task<Dictionary<Hashtag, RecommendationWeight>> GetLessInterestedHashtags(UserId userId, CancellationToken cancellationToken = default);
    Task<List<UserId>> GetRecommendedProfiles(UserId userId, CancellationToken cancellationToken = default);

    Task CreateCountryRecommendation(string country, List<Hashtag> hashtags, CancellationToken cancellationToken = default);
    Task IncrementCountryRecommendation(string country, List<Hashtag> hashtags, PostActionType postActionType, CancellationToken cancellationToken = default);

    Task<Dictionary<Hashtag, RecommendationWeight>> GetRecommendedHashtagsByUserId(UserId userId, CancellationToken cancellationToken = default);
}