using AutoMapper;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;

public class GetIngredientQueryHandler(IIngrediantsRepository ingredientsRepository, IMapper mapper)
    : IQueryHandler<GetIngredientQuery, IngredientResponse>
{
    private readonly IMapper _mapper = mapper;
    public async Task<Result<IngredientResponse>> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientsRepository.GetIngrediantByIdAsync(request.IngredientId);
        

        if (ingredient is not null)
        {
            var ingredientResponse = new IngredientResponse(
                Id: ingredient.Id,
                Name: ingredient.Name.Value,
                DisponibleIn: ingredient.DisponibleIn.Select(l => l.Name).ToList(),
                Price: ingredient.Price.Value
            );
            return Result.Success(ingredientResponse!);
        }

        return Result.Failure<IngredientResponse>(IngredientErrors.IngredientNotFound);

    }
}