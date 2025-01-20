using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.UpdateIngredient;

public class UpdateIngredientCommandHandler(IIngrediantsRepository ingrediantsRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateIngredientCommand, IngredientResponse>
{
    public async Task<Result<IngredientResponse>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredientExist = await ingrediantsRepository.GetIngrediantByIdAsync(request.Id);
        if (ingredientExist is null)
        {
            return Result.Failure<IngredientResponse>(IngredientErrors.IngredientNotFound);
        }

        if (request.Name is not null)
        {
            ingredientExist.UpdateName(request.Name);
        }

        if (request.DisponibleIn is not null)
        {
            ingredientExist.UpdateLocation(request.DisponibleIn) ;
        }

        if (request.PriceValue > 0 )
        {
            ingredientExist.UpdatePrice(request.PriceValue, request.PriceCurrency);
        }
        await ingrediantsRepository.UpdateIngredientAsync(ingredientExist);
        await unitOfWork.CommitChangesAsync();
       
        return Result.Success(new IngredientResponse(ingredientExist.Id,
            ingredientExist.Name,
            ToDto(ingredientExist.DisponibleIn),
            ingredientExist.Price.Value));
    }
    private static List<string> ToDto(List<Location> locations)
    {
        List<string> result = new();
        foreach (var location in locations)
        {
            result.Add(location.Name);
        }

        return result;

    }
}