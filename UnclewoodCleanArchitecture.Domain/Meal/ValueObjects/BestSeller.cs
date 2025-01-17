using System.Text.Json.Serialization;

namespace UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

public sealed class BestSeller : Common.Models.ValueObject
{
    public bool Value { get; private set; }

    [JsonConstructor]
    public BestSeller(bool value = false)
    {
        Value = value;
    }
    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value;
    }

    // Optional: Override ToString() for easier representation
    public override string ToString() => Value.ToString();
}