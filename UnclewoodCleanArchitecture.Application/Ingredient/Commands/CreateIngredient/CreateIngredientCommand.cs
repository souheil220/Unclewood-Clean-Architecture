using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public record CreateIngredientCommand(string Name,
    List<Location> DisponibleIn,
    Price Price
     ): ICommand<Domain.Ingredient.Ingredient>;