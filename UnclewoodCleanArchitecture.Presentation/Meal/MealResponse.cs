using UnclewoodCleanArchitecture.Application.DTOS;

namespace UnclewoodCleanArchitectur.Presentation.Meal;

public record MealResponse( Guid Id ,
    string Name,
    string Description ,
    bool BestSeller ,
    bool Promotion ,
    double PromotionRate ,
    string Category ,
    ICollection<PriceDto> Prices ,
    ICollection<MealIngredientDto> Ingrediants ,
    List<PhotoDto> Photos );