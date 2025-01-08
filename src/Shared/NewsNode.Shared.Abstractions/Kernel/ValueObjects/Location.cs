using NewsNode.Shared.Abstractions.Exception;
using NewsNode.Shared.Abstractions.Kernel.Primitives;

namespace NewsNode.Shared.Abstractions.Kernel.ValueObjects;

public record Location : ValueObject
{
    public string Country { get; }
    public string City { get; }


    public Location(string country, string city)
    {
        if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city))
            throw new DomainException("Country and city must be provided");

        Country = country;
        City = city;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Country;
        yield return City;
    }
}