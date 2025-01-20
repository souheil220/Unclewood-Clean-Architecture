using UnclewoodCleanArchitecture.Domain.Exepptions.Common;

namespace UnclewoodCleanArchitecture.Domain.Common.ValueObject;

public class BasePrice : Models.ValueObject
{
    private const decimal MinValue = 100m;
    private const decimal MaxValue = 10000m;
    public decimal Value { get; }
    public string Currency { get; init; }

    protected BasePrice(decimal value, string currency)
    {
        Value = decimal.Round(value, 2);
     var currencyValidationResult = ValidateCurrency(currency);
     if (currencyValidationResult.IsFailure)
     {
         throw new PriceDomainException(currencyValidationResult.Error.Name);
     }

     Currency = currencyValidationResult.Value;
    }
    public static BasePrice Create(decimal value, string currency)
    {
        ValidatePrice(value);
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
    
    private static Result<string> ValidateCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            return Result.Failure<string>(
                new Error(
                    "PriceDomainException.ValidateCurrency",
                    "Currency cannot be empty"));

        }

        var normalizedCurrency = currency.Trim().ToUpper();
        var validCurrencies = new[] { "DZD", "USD", "EUR" };
        
        if (!validCurrencies.Contains(normalizedCurrency))
        {
            return Result.Failure<string>(
                new Error(
                    "PriceDomainException.ValidateCurrency",
                    "Currency {currency} is not supported"));
            
        }

        return Result.Success(normalizedCurrency).Value;
    }

    private Result<BasePrice> MultiplyBy(decimal multiplier)
    {
        if (multiplier < 0)
        {
            return Result.Failure<BasePrice>(
                new Error(
                    "PriceDomainException.MultiplyBy",
                    "Multiplier cannot be negative"));

        }

        var result = Create(Value * multiplier, Currency);
        
        return Result.Success(result);
    }
    
    public Result<BasePrice> ApplyDiscount(decimal percentageDiscount)
    {
        var discountMultiplier = 1 - (percentageDiscount / 100);
        
        var result = MultiplyBy(discountMultiplier);
        if (result.IsFailure)
        {
            return Result.Failure<BasePrice>(result.Error);
        }
        else
        {
            return result.Value;
        }
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
    
    protected BasePrice(){}
    
}