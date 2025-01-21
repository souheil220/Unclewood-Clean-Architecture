using System.Collections;
using AutoMapper;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Exceptions;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Errors;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.UpdateMeal;

//TODO IMPLEMENT UPDATE MEAL
public class UpdateMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICommandHandler<UpdateMealCommand , MealResponse>
{
    public async Task<Result<MealResponse>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var mealExist = await mealRepository.GetMealByGuidAsync(request.MealId);
        if (mealExist == null)
        {
            return Result.Failure<MealResponse>(MealErrors.NotFound);
        }
        
        var prices = request.Prices is not null? mapper.Map<ICollection<Price>>(request.Prices): null;
        var pictures = request.Prices is not null ? mapper.Map<List<Photo>>(request.MealPictures) : null;

        try
        {
            if (request.Name is not null)
            {
                mealExist.UpdateName(request.Name);
            }

            if (request.Description is not null)
            {
                mealExist.UpdateDesciption(request.Description);
            }

            if (request.Category is not null)
            {
                mealExist.UpdateCategory(request.Category);
            }

            if (request.BestSeller.HasValue)
            {
                mealExist.UpdateBestSeller(request.BestSeller.Value);
            }

            if (request.Promotion.HasValue)
            {
                mealExist.UpdatePromotion(request.Promotion.Value);
            }

            if (request.PromotionRate is not null)
            {
                mealExist.UpdatePromotionRate(request.PromotionRate.Value);
            }

            if (request.Category is not null)
            {
                mealExist.UpdateCategory(request.Category);
            }

            if (prices is not null && prices.Count > 0)
            {
                mealExist.UpdatePrices(prices);
            }

            if (pictures is not null && pictures.Count > 0)
            {
                mealExist.UpdatePictures(pictures);
            }

            if (request.IngrediantsIDs is not null && request.IngrediantsIDs.Count > 0)
            {
                mealExist.UpdateIngredients(request.IngrediantsIDs);
            }
        
            await mealRepository.UpdateMealAsync(mealExist);
            await unitOfWork.CommitChangesAsync();
            return Result.Success<MealResponse>(new MealResponse(
                mealExist.Id,
                mealExist.Name.Value,
                mealExist.Description.Value,
                mealExist.BestSeller.Value,
                mealExist.Promotion.Value,
                mapper.Map<IEnumerable<PriceDto>>(mealExist.NewPrices),
                mealExist.Category.Name,
                mapper.Map<ICollection<PriceDto>>(mealExist.Prices),
                mapper.Map<ICollection<MealIngredientDto>>(mealExist.MealIngredients),
                mapper.Map<List<PhotoDto>>(mealExist.Photos)
                ));
        }
        catch (Exception domainException)
        {
            throw new DomainException([new DomainError(domainException.Message)]);
        }
       
    }
}