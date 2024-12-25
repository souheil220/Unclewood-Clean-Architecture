using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

public class DeleteMealCommandHandler:IRequestHandler<DeleteMealCommand, bool>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork)
    {
        _mealRepository = mealRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        await _mealRepository.DeleteMealAsync(request.meal);
        await _unitOfWork.CommitChangesAsync();
        //TODO Change to return a result
        return true;
    }
}