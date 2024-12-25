using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;

public class ListMealQueryHandler : IRequestHandler<ListMealQuery, IEnumerable<Domain.Meal.Meal>>
{
    private readonly IMealRepository _mealRepository;
    public ListMealQueryHandler(IMealRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }
    public async Task<IEnumerable<Domain.Meal.Meal>> Handle(ListMealQuery request, CancellationToken cancellationToken)
    {
        var meals = await _mealRepository.GetMealsAsync();
        return meals;
    }
}