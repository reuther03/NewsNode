using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Shared.Abstractions.Kernel.ValueObjects;

public record Location : ValueObject
{
    public string Country { get; }
    public string? City { get; }


    public Location(string country, string? city)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country cannot be empty");

        Country = country;
        City = city ?? "None";
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City ?? "None";
    }
}