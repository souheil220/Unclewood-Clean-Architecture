using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;

public class DeleteMealCommandHandler:ICommandHandler<DeleteMealCommand>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork)
    {
        _mealRepository = mealRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DeleteMealCommand request, CancellationToken cancellationToken)
    {
        await _mealRepository.DeleteMealAsync(request.MealId);
        await _unitOfWork.CommitChangesAsync();
        return Result.Success();
    }
}