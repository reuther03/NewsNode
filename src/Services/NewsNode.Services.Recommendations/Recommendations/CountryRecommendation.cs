using NewsNode.Shared.Abstractions.Kernel.ValueObjects;

namespace NewsNode.Services.Recommendations.Recommendations;

public class CountryRecommendation : Recommendation
{
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Country { get; private set; }

    private CountryRecommendation()
    {
    }

    public CountryRecommendation(Guid id, Hashtag hashtag, string country, DateTime lastInteraction, int score, RecommendationWeight weight)
        : base(id, hashtag, lastInteraction, score, weight)
    {
        Country = country;
    }

    public static CountryRecommendation Create(string country, Hashtag hashtag)
        => new(Guid.NewGuid(), hashtag, country, DateTime.UtcNow.Date, 0, RecommendationWeight.None);
}