using AutoMapper;
using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;
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

        var meal =  Domain.Meal.Meal.Create(
                             Name.Create(request.Name), 
                             new Descriptiion(request.Description),
                             new BestSeller(request.BestSeller),
                             new Promotion(request.Promotion),
                             PromotionRate.Create(request.PromotionRate),
                             ToDto(request.Category)
                            );
        
        var photos = mapper.Map<List<Photo>>(request.MealPictures);
        
        meal.AddIngredients(request.IngrediantsIDs);
        
        meal.AddPhotos(photos);
        
        meal.AddPrices(EnumConverter(request.Prices));
        
        await mealRepository.AddMealAsync(meal);
    
        await unitOfWork.CommitChangesAsync();

        return meal;
    }
    private static Category ToDto(string category)
    {
        return category switch
        {
            nameof(Category.Pizza) => Category.Pizza,
            nameof(Category.Sandwich) => Category.Sandwich,
            nameof(Category.Coffee) => Category.Coffee,
            _ => throw new InvalidOperationException(),
        };
    }
    private static ICollection<Price> EnumConverter(ICollection<PriceDto> prices)
    {
        List<Price> result = new();
        foreach (var price in prices)
        {
            Location.TryFromName(
                price.Location,
                out var newLocation);

           result.Add(new Price(price.Value, price.Currency, newLocation));
        }
        return result;
    }
    
   
}