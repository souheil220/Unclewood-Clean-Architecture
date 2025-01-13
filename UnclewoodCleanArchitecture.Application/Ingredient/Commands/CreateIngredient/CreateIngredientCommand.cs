using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public record CreateIngredientCommand(string Name,
    List<string> DisponibleIn,
    decimal PriceValue,
    string PriceCurrency
     ): ICommand<Domain.Ingredient.Ingredient>;