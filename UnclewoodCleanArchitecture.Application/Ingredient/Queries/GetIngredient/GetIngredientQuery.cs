using MediatR;
using UnclewoodCleanArchitecture.Application.Caching;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public record GetIngredientQuery(Guid IngredientId) : ICachedQuery<IngredientResponse>
{
    public string CacheKey => $"Ingredient-{IngredientId}";
    public TimeSpan? Expiration => null;
}