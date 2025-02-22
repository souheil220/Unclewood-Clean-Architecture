using UnclewoodCleanArchitectur.Presentation.Common;
using UnclewoodCleanArchitectur.Presentation.Common.Enums;
using UnclewoodCleanArchitecture.Application.DTOS;

namespace UnclewoodCleanArchitectur.Presentation.Meal;

public record CreateMealRequest(

        string Name, 
        ICollection<PriceDto> Prices, 
        string Description, 
        bool BestSeller, 
        bool Promotion, 
        double PromotionRate,
        List<PhotoDto> MealPictures,
        List<Guid> IngrediantsIDs,
        string Category 
);