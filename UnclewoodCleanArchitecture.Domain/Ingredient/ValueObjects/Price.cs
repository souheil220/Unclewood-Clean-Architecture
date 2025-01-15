using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;

namespace UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

[NotMapped]
public sealed class Price: BasePrice
{
    private Price() : base(0, string.Empty) // Default values for EF Core
    {
    }
    [JsonConstructor]
    public Price(decimal value, string currency) : base(value, currency) { }
    
}