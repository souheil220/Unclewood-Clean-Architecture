namespace UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

public class NameDomainException : DomainException<string>
{
    public NameDomainException(string message, string value) : base(message, value)
    {
    }

    public NameDomainException(string message) : base(message)
    {
    }
}