using AutoMapper;
using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Errors;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public class CreateMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : ICommandHandler<CreateMealCommand, Domain.Meal.Meal>
{
    public async Task<Result<Domain.Meal.Meal>> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
        var mealExist = await mealRepository.MealExists(request.Name);
        if (mealExist)
        {
            return Result.Failure<Domain.Meal.Meal>(MealErrors.MealAlreadyExist);
        }

        var meal = new Domain.Meal.Meal(
            name: Name.Create(request.Name), 
            description: request.Description,
            bestSeller: request.BestSeller,
            promotion: request.Promotion,
            promotionRate:request.PromotionRate,
            category: request.Category
            );
        var photos = mapper.Map<List<Photo>>(request.MealPictures);
        var prices = mapper.Map<ICollection<Price>>(request.Prices);
        meal.AddIngredients(request.IngrediantsIDs);
        meal.AddPhotos(photos);
        meal.AddPrices(prices);
        await mealRepository.AddMealAsync(meal);
        /* If I had a chain reaction for example that it should happen after the addition of a new mela
         I would raise the events just like a did below */
        meal.RaiseMealCreatedEvent();
        await unitOfWork.CommitChangesAsync();

        return meal;
    }
}