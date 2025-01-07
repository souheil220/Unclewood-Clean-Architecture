using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

public record DeleteMealCommand(Guid MealId) : ICommand;