using UnclewoodCleanArchitecture.Application.DTOS;

namespace UnclewoodCleanArchitecture.Application.Meal;

public record MealResponse( Guid Id ,
    string Name,
    string Description ,
    bool BestSeller ,
    bool Promotion ,
    IEnumerable<PriceDto> NewPrice ,
    string Category ,
    IEnumerable<PriceDto> Prices ,
    ICollection<MealIngredientDto> Ingrediants ,
    List<PhotoDto> Photos );