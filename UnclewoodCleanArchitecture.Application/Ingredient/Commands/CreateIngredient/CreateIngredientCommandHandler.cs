using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Domain.Ingredient.Ingredient>
{
    private readonly IIngrediantsRepository _ingrediantsRepository;
    private readonly IUnitOfWork _unitOfWork;


    public CreateIngredientCommandHandler(IIngrediantsRepository ingrediantsRepository, IUnitOfWork unitOfWork)
    {
        _ingrediantsRepository = ingrediantsRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Domain.Ingredient.Ingredient> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
    {
        var ingredient = new Domain.Ingredient.Ingredient(
            name:request.Name,
            price: request.Price
            );
        await _ingrediantsRepository.AddIngrediantAsync(ingredient);
        await _unitOfWork.CommitChangesAsync();

        return ingredient;
    }
}