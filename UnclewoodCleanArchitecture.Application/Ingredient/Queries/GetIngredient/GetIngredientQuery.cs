using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public record GetIngredientQuery(Guid IngredientId) : IQuery<Domain.Ingredient.Ingredient>;