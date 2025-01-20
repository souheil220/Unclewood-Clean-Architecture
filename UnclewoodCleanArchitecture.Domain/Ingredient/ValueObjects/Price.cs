using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;

namespace UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

[NotMapped]
public sealed class Price: BasePrice
{
    private Price() : base() // Default values for EF Core
    {
    }
    [JsonConstructor]
    public Price(decimal value, string currency) : base(value, currency) { }
    
}