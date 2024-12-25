using UnclewoodCleanArchitecture.Domain.Common.Models;

namespace UnclewoodCleanArchitecture.Domain.Common.Entities;

public sealed class MealIngredient : Entity
{
    public MealIngredient(Guid? id, Guid mealId,  Guid ingredientId) : base(id??Guid.NewGuid())
    {
        MealId = mealId;
        IngredientId = ingredientId;
    }

    public Guid MealId { get; private set; }
    public Meal.Meal Meal { get; private set; }

    public Guid IngredientId { get; private set; }
    public Ingredient.Ingredient Ingredient { get; private set; }
   
}