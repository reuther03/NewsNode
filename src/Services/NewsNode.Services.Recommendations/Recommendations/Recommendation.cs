using NewsNode.Shared.Abstractions.Kernel.Primitives;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class Recommendation : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public Hashtag Hashtag { get; private set; }
    public int Score { get; private set; }
    public RecommendationWeight Weight { get; private set; }

    public Recommendation(Guid id, UserId userId, Hashtag hashtag, int score, RecommendationWeight weight) : base(id)
    {
        UserId = userId;
        Hashtag = hashtag;
        Score = score;
        Weight = weight;
    }

    public static Recommendation Create(UserId userId, Hashtag hashtag)
        => new(Guid.NewGuid(), userId, hashtag, 0, RecommendationWeight.Low);

    public void IncrementScore(int score)
    {
        Score += score;
        Weight = Score switch
        {
            > 0 and < 50 => RecommendationWeight.None,
            < 50 => RecommendationWeight.Low,
            < 75 => RecommendationWeight.MediumLow,
            < 125 => RecommendationWeight.Medium,
            < 175 => RecommendationWeight.MediumHigh,
            < 250 => RecommendationWeight.High,
            _ => RecommendationWeight.VeryHigh
        };
    }
}