namespace UnclewoodCleanArchitecture.Domain.Common.ValueObject;

public class BasePrice : Models.ValueObject
{
    public const decimal MinValue = 100m;
    public const decimal MaxValue = 10000m;
    public decimal Value { get; }
    public string Currency { get; }

    protected BasePrice(decimal value, string currency)
    {
        Value = decimal.Round(value, 2);
        Currency = ValidateCurrency(currency);
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
            //TODO throw new DomainException($"Price cannot be less than {MinValue}");
        }

        if (value > MaxValue)
        {
            //TODO throw new DomainException($"Price cannot exceed {MaxValue}");
        }

        if (decimal.Round(value, 2) != value)
        {
            //TODO throw new DomainException("Price cannot have more than 2 decimal places");
        }
    }
    
    private static string ValidateCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            //TODO throw new DomainException("Currency cannot be empty");
        }

        var normalizedCurrency = currency.Trim().ToUpper();
        var validCurrencies = new[] { "DZD", "USD", "EUR" };
        
        if (!validCurrencies.Contains(normalizedCurrency))
        {
            //TODO throw new DomainException($"Currency {currency} is not supported");
        }

        return normalizedCurrency;
    }
    public BasePrice MultiplyBy(decimal multiplier)
    {
        if (multiplier < 0)
        {
            //TODO throw new DomainException("Multiplier cannot be negative");
        }
        var result = Value * multiplier;
        return Create(result, Currency);
    }
    
    public BasePrice ApplyDiscount(decimal percentageDiscount)
    {
        if (percentageDiscount < 0 || percentageDiscount > 100)
        {
            //TODO throw new DomainException("Discount percentage must be between 0 and 100");
        }

        var discountMultiplier = 1 - (percentageDiscount / 100);
        return MultiplyBy(discountMultiplier);
    }
    
    private void EnsureSameCurrency(BasePrice other)
    {
        if (Currency != other.Currency)
        {
            //TODO throw new DomainException($"Cannot compare prices with different currencies: {Currency} and {other.Currency}");
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