using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

namespace UnclewoodCleanArchitecture.Domain.Common.ValueObject;

public sealed class Name : Models.ValueObject
{
    public string Value { get; private set; }
    
    [JsonConstructor]
    public Name(string value)
    {
        Value = value;
    }
    
    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new NameDomainException("Name cannot be null or empty.", nameof(value));
        }

        if (value.Length < 3 || value.Length > 50)
        {
            throw new NameDomainException("Name must be between 3 and 50 characters long.", nameof(value));
        }
        
        value = value.Trim();
        return new Name(value.ToLower());
    }
    
    public static implicit operator string(Name name) => name.Value;

    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value.ToLower();
    }
    
    public override string ToString() => Value;
}