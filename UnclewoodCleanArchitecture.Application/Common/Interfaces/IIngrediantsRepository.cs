namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IIngrediantsRepository
{
    Task UpdateIngredientAsync(Domain.Ingredient.Ingredient ingrediant);
    Task<Domain.Ingredient.Ingredient?> GetIngrediantByNameAsync(string ingrediantName);
    Task<Domain.Ingredient.Ingredient?> GetIngrediantByIdAsync(Guid id);
    Task<IEnumerable<Domain.Ingredient.Ingredient>> GetIngrediantsAsync();
    Task<bool> IngrediantExists(string ingrediantName);
    Task AddIngrediantAsync(Domain.Ingredient.Ingredient ingrediant);
    Task DeleteIngrediantAsync(Guid ingrediantId);
}