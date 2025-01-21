using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;

public record ListMealQuery : IQuery<List<MealResponse>>;