using MediatR;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;

public record ListIngredientQuery : IRequest<IEnumerable<Domain.Ingredient.Ingredient>>;