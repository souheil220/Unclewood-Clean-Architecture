using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;

namespace UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

public sealed class Price : BasePrice
{
    public Location Location { get; private set; }
    
    private Price(decimal value, string currency, Location location)
        : base(value, currency)
    {
        Location = location;
    }
    public override IEnumerable<object?> GetEqualityComponent()
    {
        // Call base implementation to include shared properties
        foreach (var component in base.GetEqualityComponent())
        {
            yield return component;
        }

        // Include Location in equality check
        yield return Location;
    }
    
}