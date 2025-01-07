using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public class CreateIngredientCommandHandler(IIngrediantsRepository ingrediantsRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateIngredientCommand, Domain.Ingredient.Ingredient>
{
    public async Task<Result<Domain.Ingredient.Ingredient>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var mealExist = await ingrediantsRepository.IngrediantExists(request.Name);
        if (mealExist)
        {
            return Result.Failure<Domain.Ingredient.Ingredient>(IngredientErrors.IngredientAlreadyExist);
        }
        var ingredient = new Domain.Ingredient.Ingredient(
            name:Name.Create(request.Name),
            disponibleIn:request.DisponibleIn,
            price: request.Price
            );
        await ingrediantsRepository.AddIngrediantAsync(ingredient);
        await unitOfWork.CommitChangesAsync();

        return ingredient;
    }
}