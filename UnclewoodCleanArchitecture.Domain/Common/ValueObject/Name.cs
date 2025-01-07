namespace UnclewoodCleanArchitecture.Domain.Common.ValueObject;

public sealed class Name : Models.ValueObject
{
    public const int MinLength = 3;
    public const int MaxLength = 40;
    //[Range(100, 10000, ErrorMessage = "Value must be between 100 and 10000.")]
    public string Value { get; private set; }
    private Name(string value)
    {
        Value = value;
    }
    
    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            //TODO throw new DomainException("Name cannot be empty.");
        }
        if (value.Length < MinLength)
        {
           //TODO throw new DomainException($"Name must be at least {MinLength} characters long.");
        }
        if (value.Length > MaxLength)
        {
            //TODO throw new DomainException($"Name cannot exceed {MaxLength} characters.");
        }
        // You might want to add more validation rules
        if (!value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-' || c == '\''))
        {
            //TODO throw new DomainException("Name contains invalid characters.");
        }
        value = value.Trim();
        
        return new Name(value.ToLower());
    }
    public static Name CreateUnvalidated(string value)
    {
        return new Name(value);
    }
    public static implicit operator string(Name name) => name.Value;

    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value.ToLower();
    }
    
    public override string ToString() => Value;
}