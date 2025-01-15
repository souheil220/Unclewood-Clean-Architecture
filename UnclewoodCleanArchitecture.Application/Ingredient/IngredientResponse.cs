

namespace UnclewoodCleanArchitecture.Application.Ingredient;

public record IngredientResponse( Guid Id,
    string Name,
    List<string> DisponibleIn,
    decimal Price);