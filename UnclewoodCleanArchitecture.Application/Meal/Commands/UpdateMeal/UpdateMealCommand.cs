using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Application.DTOS;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.UpdateMeal;

//TODO IMPLEMENT UPDATE MEAL
public record UpdateMealCommand(
    Guid MealId,
    string? Name, 
    ICollection<PriceDto>? Prices, 
    string? Description, 
    bool? BestSeller, 
    bool? Promotion, 
    double? PromotionRate,
    List<PhotoDto>? MealPictures,
    List<Guid>? IngrediantsIDs,
    string? Category ): ICommand<MealResponse>;