using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.Ingredient.Persistence;

public class IngredientRepository : IIngrediantsRepository
{
    private readonly UnclewoodDbContext _dbContext;

    public IngredientRepository(UnclewoodDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void UpdateIngrediantAsync(Domain.Ingredient.Ingredient ingredient)
    {
        _dbContext.Entry(ingredient).State = EntityState.Modified;
    }

    public async Task<Domain.Ingredient.Ingredient?> GetIngrediantByNameAsync(string ingrediantName)
    {
        return await _dbContext.Ingredients.
            Where(i => i.Name == ingrediantName)
            .SingleOrDefaultAsync();
    }

    public async Task<Domain.Ingredient.Ingredient?> GetIngrediantByIdAsync(Guid id)
    {
        return await _dbContext.Ingredients.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.Ingredient.Ingredient>> GetIngrediantsAsync()
    {
        return (await _dbContext.Ingredients.ToListAsync())!;
    }

    public async Task<bool> IngrediantExists(string ingrediantName)
    {
        return await _dbContext.Ingredients.AnyAsync(x => x.Name == ingrediantName.ToLower());

    }

    public async Task AddIngrediantAsync(Domain.Ingredient.Ingredient ingrediant)
    {
        await _dbContext.Ingredients.AddAsync(ingrediant);
    }

    public async Task DeleteIngrediantAsync(Guid ingrediantId)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(ingrediantId);
        if (ingredient == null)
        {
            throw new KeyNotFoundException($"Ingredient with ID {ingrediantId} not found.");
        }
        _dbContext.Ingredients.Remove(ingredient);
    }
}
  