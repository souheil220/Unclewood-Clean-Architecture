using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

//TODO replace Domain.Meal.Meal by MealResponse with primitive types like strings and not like
//TODO Name and Location and Price but not for Photos and Ingredients
public record GetMealQuery(Guid MealId) : IQuery<Domain.Meal.Meal>;