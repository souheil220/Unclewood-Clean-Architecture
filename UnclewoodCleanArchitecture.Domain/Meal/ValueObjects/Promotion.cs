using System.Text.Json.Serialization;

namespace UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;



public sealed class Promotion : Common.Models.ValueObject
{
    public bool Value { get; private set; }

    [JsonConstructor]
    public Promotion(bool value = false)
    {
        Value = value;
    }
    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}