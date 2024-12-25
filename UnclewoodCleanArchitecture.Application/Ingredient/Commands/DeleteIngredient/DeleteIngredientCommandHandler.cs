using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;

public class DeleteIngredientCommandHandler:IRequestHandler<DeleteIngredientCommand, bool>
{
    private readonly IIngrediantsRepository _ingrediantsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteIngredientCommandHandler(IIngrediantsRepository ingrediants, IUnitOfWork unitOfWork)
    {
        _ingrediantsRepository = ingrediants;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
    {
        await _ingrediantsRepository.DeleteIngrediantAsync(request.Ingredient);
        await _unitOfWork.CommitChangesAsync();
        //TODO Change to return a result
        return true;
    }
}