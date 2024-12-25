using MediatR;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public record CreateIngredientCommand( Name Name,
    Price Price
     ): IRequest<Domain.Ingredient.Ingredient>;