using UnclewoodCleanArchitecture.Domain.Common.ValueObject;

namespace UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

public sealed class Price: BasePrice
{
    public Price(decimal value, string currency) : base(value, currency) { }
}