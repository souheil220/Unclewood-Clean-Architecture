using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;

namespace UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;

public class MealRepository : IMealRepository
{
    private readonly UnclewoodDbContext _dbContext;

    public MealRepository(UnclewoodDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void UpdateMeal(Domain.Meal.Meal meal)
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Meal.Meal?> GetMealByNameAsync(string mealName)
    {
        return await _dbContext.Meals
            .Where(x => x.Name == mealName)
            .SingleOrDefaultAsync();
    }

    public async Task<Domain.Meal.Meal?> GetMealByGuidAsync(Guid mealGuid)
    {
        return await _dbContext.Meals.Include(m => m!.MealIngredients) 
            .ThenInclude(mi => mi.Ingredient) 
            .Include(m => m!.Photos) 
            .FirstOrDefaultAsync(m => m!.Id == mealGuid);
    }

    public async Task<IEnumerable<Domain.Meal.Meal>> GetMealsAsync()
    {
        return await _dbContext.Meals.Include(m => m!.Prices)
            .Include(m => m!.Photos)
            .Include(m => m!.MealIngredients)
            .ThenInclude(i => i.Ingredient)
            .ToListAsync();
    }

    public async Task<bool> MealExists(string mealName)
    {
        return await _dbContext.Meals.AnyAsync(x => x.Name == mealName.ToLower());
    }

    public async Task AddMealAsync(Domain.Meal.Meal meal)
    {
       await _dbContext.Meals.AddAsync(meal);
    }

    public async Task DeleteMealAsync(Guid mealId)
    {
        var meal = await _dbContext.Meals.FindAsync(mealId);
        if (meal == null)
        {
            throw new KeyNotFoundException($"Meal with ID {mealId} not found.");
        }

        _dbContext.Meals.Remove(meal);
    }
}
  