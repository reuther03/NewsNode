namespace NewsNode.Services.Recommendations.Recommendations;

public enum RecommendationWeight
{
    None = 0,
    Low = 1,
    MediumLow = 2,
    Medium = 3,
    MediumHigh = 4,
    High = 5,
    VeryHigh = 6,

    LowNegative = -1,
    MediumNegative = -2,
    HighNegative = -3,
    VeryHighNegative = -4
}