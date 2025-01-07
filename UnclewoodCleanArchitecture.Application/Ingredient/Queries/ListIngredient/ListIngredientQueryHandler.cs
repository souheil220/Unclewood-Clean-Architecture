using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;

public class ListIngredientQueryHandler(IIngrediantsRepository ingrediantsRepository)
    : IQueryHandler<ListIngredientQuery, IEnumerable<Domain.Ingredient.Ingredient>>
{
    public async Task<Result<IEnumerable<Domain.Ingredient.Ingredient>>> Handle(ListIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingredients = await ingrediantsRepository.GetIngrediantsAsync();
        
        return Result.Success(ingredients);
    }
}