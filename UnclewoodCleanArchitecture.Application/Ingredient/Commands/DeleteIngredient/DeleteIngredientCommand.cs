using MediatR;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;

public record DeleteIngredientCommand(Guid IngredientId ) : IRequest<bool>;