using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

namespace UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

public class Descriptiion : Common.Models.ValueObject
{
    public string Value { get; private set; }
    
    [JsonConstructor]
    public Descriptiion(string value)
    {
        Value = value;
    }
    
    public static Descriptiion Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DescriptionDomainException("Description cannot be null or empty.");
        }

        if (value.Length < 10 || value.Length > 500)
        {
            throw new DescriptionDomainException("Description must be between 10 and 500 characters long.",value);
        }

        value = value.Trim();

        return new Descriptiion(value);
    }

    
    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value.ToLower();
    }
    
    public override string ToString() => Value;
}