using MediatR;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public record GetIngredientQuery(Guid IngredientId) : IRequest<Domain.Ingredient.Ingredient>;