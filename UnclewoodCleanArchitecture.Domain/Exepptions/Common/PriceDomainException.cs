namespace UnclewoodCleanArchitecture.Domain.Exepptions.Common;

public class PriceDomainException : DomainException<decimal>
{
    public PriceDomainException(string message, decimal value) : base(message, value) { }
    public PriceDomainException(string message) : base(message) { }
}