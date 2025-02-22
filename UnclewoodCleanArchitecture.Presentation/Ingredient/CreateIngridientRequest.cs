using UnclewoodCleanArchitectur.Presentation.Common;
using UnclewoodCleanArchitectur.Presentation.Common.Enums;
using DomainLocation = UnclewoodCleanArchitecture.Domain.Common.Enum.Location;
using DomainPrice = UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects.Price;

namespace UnclewoodCleanArchitectur.Presentation.Ingredient;

public record CreateIngridientRequest(string Name, List<string> DisponibleIn,
    decimal PriceValue, string PriceCurrency);