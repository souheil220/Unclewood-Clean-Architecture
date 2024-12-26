using UnclewoodCleanArchitectur.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;

namespace UnclewoodCleanArchitectur.Infrastructure.Meal.Persistence;

public class MealRepository : IMealRepository
{
    private readonly UnclewoodDbContext _dbContext;

    public MealRepository(UnclewoodDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void UpdateMeal(UnclewoodCleanArchitecture.Domain.Meal.Meal meal)
    {
        throw new NotImplementedException();
    }

    public Task<UnclewoodCleanArchitecture.Domain.Meal.Meal> GetMealByNameAsync(string mealName)
    {
        throw new NotImplementedException();
    }

    public Task<UnclewoodCleanArchitecture.Domain.Meal.Meal> GetMealByGuidAsync(Guid mealGuid)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UnclewoodCleanArchitecture.Domain.Meal.Meal>> GetMealsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> MealExists(string mealName)
    {
        throw new NotImplementedException();
    }

    public Task AddMealAsync(UnclewoodCleanArchitecture.Domain.Meal.Meal meal)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMealAsync(UnclewoodCleanArchitecture.Domain.Meal.Meal meal)
    {
        throw new NotImplementedException();
    }
}
  