using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public class GetIngredientQueryHandler(IIngrediantsRepository ingredientsRepository)
    : IQueryHandler<GetIngredientQuery, Domain.Ingredient.Ingredient>
{
    public async Task<Result<Domain.Ingredient.Ingredient>> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientsRepository.GetIngrediantByIdAsync(request.IngredientId);

        if (ingredient is null)
        {
            Result.Failure<Domain.Ingredient.Ingredient>(IngredientErrors.IngredientNotFound);
        }

        return Result.Success(ingredient!);

    }
}