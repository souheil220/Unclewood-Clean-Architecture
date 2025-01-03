using MediatR;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public record CreateMealCommand( 
    string Name, 
    ICollection<PriceDto> Prices, 
    string Description, 
    bool BestSeller, 
    bool Promotion, 
    double PromotionRate,
    List<PhotoDto> MealPictures,
    List<Guid> IngrediantsIDs,
    Category Category ): IRequest<Domain.Meal.Meal>;