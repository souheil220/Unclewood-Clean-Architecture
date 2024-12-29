using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitectur.Presentation.Common.Enums;

namespace UnclewoodCleanArchitectur.Presentation.DTOs;

public record MealDto(
     Guid Id ,
     string Name,
     string Description ,
     bool BestSeller ,
     bool Promotion ,
     double PromotionRate ,
     Category Category ,
     ICollection<PriceDto> Prices ,
     ICollection<MealIngredientDto> Ingrediants ,
     List<PhotoDto> Photos);