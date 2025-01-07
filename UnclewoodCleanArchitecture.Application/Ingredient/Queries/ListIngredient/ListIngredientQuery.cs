using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;

public record ListIngredientQuery : IQuery<IEnumerable<Domain.Ingredient.Ingredient>>;