namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IMealRepository
{
    void UpdateMeal(Domain.Meal.Meal meal);
    Task<Domain.Meal.Meal?> GetMealByNameAsync(string mealName);
    Task<Domain.Meal.Meal?> GetMealByGuidAsync(Guid mealGuid);
    Task<IEnumerable<Domain.Meal.Meal>> GetMealsAsync();
    Task<bool> MealExists(string mealName);
    Task AddMealAsync(Domain.Meal.Meal meal);
    Task DeleteMealAsync(Guid mealId);
}