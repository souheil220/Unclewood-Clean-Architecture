namespace UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

public class DescriptionDomainException: DomainException<string>
{
    public DescriptionDomainException(string message, string value) : base(message, value) { }
    public DescriptionDomainException(string message) : base(message) { }
    
}