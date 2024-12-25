using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public class GetIngredientQueryHandler: IRequestHandler<GetIngredientQuery, Domain.Ingredient.Ingredient>
{
    private readonly IIngrediantsRepository _ingrediantsRepository;
    
    public GetIngredientQueryHandler(IIngrediantsRepository ingrediantsRepository)
    {
        _ingrediantsRepository = ingrediantsRepository;
    }
    public async Task<Domain.Ingredient.Ingredient> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingrediant = await _ingrediantsRepository.GetIngrediantByIdAsync(request.IngredientId);

        return ingrediant;
    }
}