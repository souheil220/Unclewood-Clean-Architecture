using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public record CreateMealCommand(
    string Name,
    ICollection<PriceDto> Prices,
    string Description,
    List<PhotoDto> MealPictures,
    List<Guid> IngrediantsIDs,
    string Category,
    bool BestSeller = false,
    bool Promotion = false,
    double PromotionRate = 0): ICommand<Domain.Meal.Meal>;