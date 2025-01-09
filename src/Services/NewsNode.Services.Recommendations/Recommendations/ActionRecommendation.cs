using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class ActionRecommendation : Recommendation
{
    public UserId UserId { get; private set; }

    private ActionRecommendation(Guid id, UserId userId, Hashtag hashtag, DateTime lastInteraction, int score, RecommendationWeight weight)
        : base(id, hashtag, lastInteraction, score, weight)
    {
        UserId = userId;
    }

    public static ActionRecommendation Create(UserId userId, Hashtag hashtag)
        => new(Guid.NewGuid(), userId, hashtag, DateTime.UtcNow.Date, 0, RecommendationWeight.None);

}