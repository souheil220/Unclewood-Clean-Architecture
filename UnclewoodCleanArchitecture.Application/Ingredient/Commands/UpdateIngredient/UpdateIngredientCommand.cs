using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.UpdateIngredient;

public record UpdateIngredientCommand(
    Guid Id,
    string? Name,
    List<string>? DisponibleIn,
    string PriceCurrency = "DZD",
    decimal PriceValue = 0): ICommand<IngredientResponse>;