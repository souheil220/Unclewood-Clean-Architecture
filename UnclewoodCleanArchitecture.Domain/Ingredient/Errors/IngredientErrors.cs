using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Domain.Ingredient.Errors;

public class IngredientErrors
{
    public static Error IngredientNotFound => new(
        "Ingredient.Found",
        "The ingredient with the specified id doesn't exist."
    );
    
    public static Error IngredientAlreadyExist => new(
        "Ingredient.Exist",
        "The ingredient specified already exist."
    );
}