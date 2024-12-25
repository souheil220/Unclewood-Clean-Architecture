namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IMealRepository
{
    void UpdateMeal(Domain.Meal.Meal meal);
    Task<Domain.Meal.Meal> GetMealByNameAsync(string mealName);
    Task<Domain.Meal.Meal> GetMealByGuidAsync(Guid mealGuid);
    Task<IEnumerable<Domain.Meal.Meal>> GetMealsAsync();
   //TODO A verfier si c'est ici ou bien dans Meal aggregate root 
    Task<bool> MealExists(string mealName);
   // void AddMealIngrediant(ICollection<MealIngredeant> mealIngrediant);
    Task AddMealAsync(Domain.Meal.Meal meal);
    Task DeleteMealAsync(Domain.Meal.Meal meal);
}