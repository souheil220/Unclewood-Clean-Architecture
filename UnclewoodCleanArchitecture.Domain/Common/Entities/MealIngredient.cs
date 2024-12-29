using UnclewoodCleanArchitecture.Domain.Common.Models;

namespace UnclewoodCleanArchitecture.Domain.Common.Entities;

public sealed class MealIngredient : Models.ValueObject
{
    public MealIngredient(Guid mealId,  Guid ingredientId)
    {
        MealId = mealId;
        IngredientId = ingredientId;
    }

    public Guid MealId { get; private set; }
    public Meal.Meal Meal { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient.Ingredient Ingredient { get; private set; }

    public override IEnumerable<object?> GetEqualityComponent()
    {
        yield return MealId;
        yield return IngredientId;
    }
}