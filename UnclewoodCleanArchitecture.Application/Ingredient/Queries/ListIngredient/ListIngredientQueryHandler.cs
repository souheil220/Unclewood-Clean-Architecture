using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;

public class ListIngredientQueryHandler : IRequestHandler<ListIngredientQuery, IEnumerable<Domain.Ingredient.Ingredient>>
{
    private readonly IIngrediantsRepository _ingrediantsRepository;
    public ListIngredientQueryHandler(IIngrediantsRepository ingrediantsRepository)
    {
        _ingrediantsRepository = ingrediantsRepository;
    }
    public async Task<IEnumerable<Domain.Ingredient.Ingredient>> Handle(ListIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingrediants = await _ingrediantsRepository.GetIngrediantsAsync();
        return ingrediants;
    }
}