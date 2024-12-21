using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Modules.Socials.Domain.Post;

public record Hashtag : ValueObject
{
    public string Value { get; set; }

    public Hashtag(string value)
    {
        if (value[0] is not '#')
        {
            value = "#" + value;
        }

        Value = value;
    }

    public static implicit operator Hashtag(string value) => new(value);
    public static implicit operator string(Hashtag hashtag) => hashtag.Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}