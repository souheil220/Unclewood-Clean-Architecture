using MediatR;
using UnclewoodCleanArchitecture.Domain.Meal;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

public record GetMealQuery(Guid MealId) : IRequest<Domain.Meal.Meal>;