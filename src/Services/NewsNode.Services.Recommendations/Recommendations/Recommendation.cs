using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class Recommendation : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public Hashtag Hashtag { get; private set; }
    public DateTime LastInteraction { get; private set; }
    public int Score { get; private set; }
    public RecommendationWeight Weight { get; private set; }

    public Recommendation(Guid id, UserId userId, Hashtag hashtag, int score, RecommendationWeight weight) : base(id)
    {
        UserId = userId;
        Hashtag = hashtag;
        LastInteraction = DateTime.UtcNow.Date;
        Score = score;
        Weight = weight;
    }

    public static Recommendation Create(UserId userId, Hashtag hashtag)
        => new(Guid.NewGuid(), userId, hashtag, 0, RecommendationWeight.None);

    public void IncrementScore(PostActionType postActionType)
    {
        var score = postActionType switch
        {
            PostActionType.Disliked => -1,
            PostActionType.NotInterested => -5,
            PostActionType.ShowLess => -10,
            PostActionType.Liked => 2,
            PostActionType.Reposted => 5,
            PostActionType.Bookmarked => 4,
            PostActionType.Commented => 3,
            _ => 0
        };
        Score += score;
        SetWeight(Score);
        LastInteraction = DateTime.UtcNow;
    }

    public void SetScore(int score)
    {
        Score = score;
        SetWeight(score);
    }

    private void SetWeight(int score)
    {
        Weight = score switch
        {
            <= -800 => RecommendationWeight.VeryHighNegative,
            <= -400 => RecommendationWeight.HighNegative,
            <= -200 => RecommendationWeight.MediumNegative,
            <= -50 => RecommendationWeight.LowNegative,
            > 0 and < 25 => RecommendationWeight.None,
            < 50 => RecommendationWeight.Low,
            < 100 => RecommendationWeight.MediumLow,
            < 200 => RecommendationWeight.Medium,
            < 400 => RecommendationWeight.MediumHigh,
            < 800 => RecommendationWeight.High,
            _ => RecommendationWeight.VeryHigh
        };
        LastInteraction = DateTime.UtcNow;
    }
}