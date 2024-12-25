using MediatR;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public record CreateMealCommand( 
    Name Name, 
    ICollection<Price> Prices, 
    string Description, 
    bool BestSeller, 
    bool Promotion, 
    double PromotionRate,
    List<PhotoDto> MealPictures,
    List<Guid> IngrediantsIDs,
    Category Category ): IRequest<Domain.Meal.Meal>;