using MediatR;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public record CreateIngredientCommand(string Name,
    List<Location> DisponibleIn,
    Price Price
     ): IRequest<Domain.Ingredient.Ingredient>;