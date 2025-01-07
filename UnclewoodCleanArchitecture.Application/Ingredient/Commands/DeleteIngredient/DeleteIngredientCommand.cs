using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;

public record DeleteIngredientCommand(Guid IngredientId ) : ICommand;