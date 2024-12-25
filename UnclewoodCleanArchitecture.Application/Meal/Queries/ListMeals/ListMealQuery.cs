using MediatR;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;

public record ListMealQuery : IRequest<IEnumerable<Domain.Meal.Meal>>;