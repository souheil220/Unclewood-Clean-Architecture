namespace UnclewoodCleanArchitecture.Domain.Exepptions.Meal;

public class PromotionRateDomainException : DomainException<double>
{
    public PromotionRateDomainException(string message, double value) : base(message, value)
    {
    }

    public PromotionRateDomainException(string message) : base(message)
    {
    }
}