using UnclewoodCleanArchitectur.Presentation.Common.Enums;

namespace UnclewoodCleanArchitectur.Presentation.Ingredient;

public record IngredientResponse( Guid Id,
    string Name,
    List<string> DisponibleIn,
    decimal Price);