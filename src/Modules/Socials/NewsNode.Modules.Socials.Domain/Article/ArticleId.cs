using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Modules.Socials.Domain.Article;

public record ArticleId : EntityId
{

    public ArticleId(Guid value) : base(value)
    {
    }

    public static ArticleId New() => new(Guid.NewGuid());
    public static ArticleId From(Guid value) => new(value);
    public static ArticleId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(ArticleId userId) => userId.Value;
    public static implicit operator ArticleId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}