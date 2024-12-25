using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

public class GetMealQueryHandler: IRequestHandler<GetMealQuery, Domain.Meal.Meal>
{
    private readonly IMealRepository _mealRepository;
    
    public GetMealQueryHandler(IMealRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }
    public async Task<Domain.Meal.Meal> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
        var meal = await _mealRepository.GetMealByGuidAsync(request.MealId);

        return meal;
    }
}