using UnclewoodCleanArchitecture.Domain.Exepptions.Common;

namespace UnclewoodCleanArchitecture.Domain.Common.ValueObject;

public class BasePrice : Models.ValueObject
{
    private const decimal MinValue = 100m;
    private const decimal MaxValue = 10000m;
    public decimal Value { get; }
    public string Currency { get; }

    protected BasePrice(decimal value, string currency)
    {
        Value = decimal.Round(value, 2);
        Currency = !string.IsNullOrWhiteSpace(currency)? ValidateCurrency(currency):"DZA";
    }
    public static BasePrice Create(decimal value, string currency)
    {
        ValidatePrice(value);
        return new BasePrice(value, currency);
    }
    public static BasePrice CreateUnvalidated(decimal value, string currency)
    {
        return new BasePrice(value, currency);
    }
    
    private static void ValidatePrice(decimal value)
    {
        if (value < MinValue)
        {
            throw new PriceDomainException($"Price cannot be less than {MinValue}");
        }

        if (value > MaxValue)
        {
            throw new PriceDomainException($"Price cannot exceed {MaxValue}");
        }

        if (decimal.Round(value, 2) != value)
        {
            throw new PriceDomainException("Price cannot have more than 2 decimal places");
        }
    }
    
    private static string ValidateCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new PriceDomainException("Currency cannot be empty");
        }

        var normalizedCurrency = currency.Trim().ToUpper();
        var validCurrencies = new[] { "DZD", "USD", "EUR" };
        
        if (!validCurrencies.Contains(normalizedCurrency))
        {
            throw new PriceDomainException($"Currency {currency} is not supported");
        }

        return normalizedCurrency;
    }

    private BasePrice MultiplyBy(decimal multiplier)
    {
        if (multiplier < 0)
        {
            throw new PriceDomainException("Multiplier cannot be negative");
        }
        var result = Value * multiplier;
        return Create(result, Currency);
    }
    
    public BasePrice ApplyDiscount(decimal percentageDiscount)
    {
        var discountMultiplier = 1 - (percentageDiscount / 100);
        
        return MultiplyBy(discountMultiplier);
    }
    
    private void EnsureSameCurrency(BasePrice other)
    {
        if (Currency != other.Currency)
        {
            throw new PriceDomainException($"Cannot compare prices with different currencies: {Currency} and {other.Currency}");
        }
    }

    public bool IsGreaterThan(BasePrice other)
    {
        EnsureSameCurrency(other);
        return Value > other.Value;
    }

    public bool IsLessThan(BasePrice other)
    {
        EnsureSameCurrency(other);
        return Value < other.Value;
    }
    
    
    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return Value;
        yield return Currency;
    }
    
}