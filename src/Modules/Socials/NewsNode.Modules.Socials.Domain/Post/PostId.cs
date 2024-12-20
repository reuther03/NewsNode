using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Modules.Socials.Domain.Post;

public record PostId : EntityId
{

    public PostId(Guid value) : base(value)
    {
    }

    public static PostId New() => new(Guid.NewGuid());
    public static PostId From(Guid value) => new(value);
    public static PostId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(PostId userId) => userId.Value;
    public static implicit operator PostId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}