using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class ActionRecommendation : Recommendation
{
    public UserId UserId { get; private set; }
    public Hashtag Hashtag { get; private set; }

    private ActionRecommendation()
    {
    }

    private ActionRecommendation(Guid id, UserId userId, Hashtag hashtag, DateTime lastInteraction, int score, RecommendationWeight weight)
        : base(id, lastInteraction, score, weight, nameof(ActionRecommendation))
    {
        UserId = userId;
        Hashtag = hashtag;
    }

    public static ActionRecommendation Create(UserId userId, Hashtag hashtag)
        => new(Guid.NewGuid(), userId, hashtag, DateTime.UtcNow.Date, 0, RecommendationWeight.None);
}