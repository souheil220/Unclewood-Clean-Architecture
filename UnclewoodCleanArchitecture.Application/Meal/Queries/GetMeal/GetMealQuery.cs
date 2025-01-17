using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

public record GetMealQuery(Guid MealId) : IQuery<MealResponse>;