using MediatR;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;

public record DeleteIngredientCommand(Domain.Ingredient.Ingredient Ingredient ) : IRequest<bool>;