using MediatR;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

public record DeleteMealCommand(Guid MealId) : IRequest<bool>;