using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

namespace UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

public sealed class PromotionRate : Common.Models.ValueObject
{
    public double Value { get; private set; }

    [JsonConstructor]
    private PromotionRate(double value)
    {
        Value = value;
    }

    public static PromotionRate Create(double value)
    {
        if (value < 0 || value > 100)
        {
            throw new PromotionRateDomainException("Promotion rate must be between 0 and 100.", value);
        }

        return new PromotionRate(value);
    }

    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}