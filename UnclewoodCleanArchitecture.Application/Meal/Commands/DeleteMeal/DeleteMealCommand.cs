using MediatR;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

public record DeleteMealCommand(Domain.Meal.Meal meal) : IRequest<bool>;