using AutoMapper;
using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public class CreateMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateMealCommand, Domain.Meal.Meal>
{
    public async Task<Domain.Meal.Meal> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
       

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