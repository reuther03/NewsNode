using NewsNode.Shared.Abstractions.Kernel.ValueObjects;
using NewsNode.Shared.Abstractions.Kernel.ValueObjects.Ids;

namespace NewsNode.Services.Recommendations.Recommendations;

public class LocalRecommendation : Recommendation
{
    public string Country { get; private set; }

    public LocalRecommendation(Guid id, Hashtag hashtag, string country, DateTime lastInteraction, int score, RecommendationWeight weight)
        : base(id, hashtag, lastInteraction, score, weight)
    {
        Country = country;
    }

    public static LocalRecommendation Create(string country, Hashtag hashtag)
        => new(Guid.NewGuid(), hashtag, country, DateTime.UtcNow.Date, 0, RecommendationWeight.None);
}