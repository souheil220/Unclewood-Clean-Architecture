using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

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
            disponibleIn: EnumConverter(request.DisponibleIn),
            price: new Price(request.PriceValue,request.PriceCurrency)
            );
        await ingrediantsRepository.AddIngrediantAsync(ingredient);
        await unitOfWork.CommitChangesAsync();

        return ingredient;
    }
    
    private static List<Location> EnumConverter(List<string> locations)
    {
        List<Location> result = new();
        foreach (var location in locations)
        {
            Location.TryFromName(
                location.ToString(),
                out var newLocation);
            result.Add(newLocation);
        }
        return result;
    }
}