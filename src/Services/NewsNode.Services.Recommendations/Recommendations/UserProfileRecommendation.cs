using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class UserProfileRecommendation : Recommendation
{
    public UserId UserId { get; private set; }
    public UserId TargetUserId { get; private set; }

    private UserProfileRecommendation()
    {
    }

    private UserProfileRecommendation(Guid id, UserId userId, UserId targetUserId, DateTime lastInteraction, int score, RecommendationWeight weight)
        : base(id, lastInteraction, score, weight, nameof(UserProfileRecommendation))
    {
        UserId = userId;
        TargetUserId = targetUserId;
    }

    public static UserProfileRecommendation Create(UserId userId, UserId targetUserId)
        => new(Guid.NewGuid(), userId, targetUserId, DateTime.UtcNow.Date, 0, RecommendationWeight.None);

}