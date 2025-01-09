using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Services.Recommendations.Recommendations;

public record RecommendationId : EntityId
{
    public RecommendationId(Guid value) : base(value)
    {
    }

    public static RecommendationId New() => new(Guid.NewGuid());
    public static RecommendationId From(Guid value) => new(value);
    public static RecommendationId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(RecommendationId recommendationId) => recommendationId.Value;
    public static implicit operator RecommendationId(Guid recommendationId) => new(recommendationId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}