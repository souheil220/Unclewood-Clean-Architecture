using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.Common.Enums;
using UnclewoodCleanArchitectur.Presentation.DTOs;
using UnclewoodCleanArchitectur.Presentation.Meal;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;
using UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;
using UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;
using DomainCategory = UnclewoodCleanArchitecture.Domain.Meal.Enums.Category;



namespace UnclewoodCleanArchitectur.Presentation.Controllers.Meal;

public class MealController : BaseApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public MealController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        

    }
    
    [HttpPost]
    public async Task<ActionResult> CreateMeal([FromBody] CreateMealRequest mealRequest)
    {
        if (!DomainCategory.TryFromName(
                mealRequest.Category.ToString(),
                out var category))
        {
           return BadRequest("Could not Convert");
        }

        var command = new CreateMealCommand(
            mealRequest.Name.Value,
            mealRequest.Prices,
            mealRequest.Description,
            mealRequest.BestSeller,
            mealRequest.Promotion,
            mealRequest.PromotionRate,
            mealRequest.MealPictures,
            mealRequest.IngrediantsIDs,
            category);
        
        var createMealResult = await _mediator.Send(command);
        
        var prices = _mapper.Map<ICollection<PriceDto>>(createMealResult.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(createMealResult.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(createMealResult.Photos);
        
        return Ok(new MealResponse(
            new MealDto(
                Id:createMealResult.Id,
                Name:createMealResult.Name.Value,
                Description:createMealResult.Description,
                BestSeller:createMealResult.BestSeller,
                Promotion: createMealResult.Promotion,
                PromotionRate: createMealResult.PromotionRate,
                Category: ToDto(createMealResult.Category),
                Prices : prices,
                Ingrediants: mealIngredients,
                Photos: mealPhotos)));
    }
    
    [HttpGet("{mealId:guid}")]
    public async Task<IActionResult> GetMeal(Guid mealId)
    {
        var query = new GetMealQuery(mealId);

        var getMealResult = await _mediator.Send(query);
        
        var prices = _mapper.Map<ICollection<PriceDto>>(getMealResult.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(getMealResult.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(getMealResult.Photos);

        return Ok(new MealResponse(
            new MealDto(
                Id:getMealResult.Id,
                Name:getMealResult.Name.Value,
                Description:getMealResult.Description,
                BestSeller:getMealResult.BestSeller,
                Promotion: getMealResult.Promotion,
                PromotionRate: getMealResult.PromotionRate,
                Category: ToDto(getMealResult.Category),
                Prices : prices,
                Ingrediants: mealIngredients,
                Photos: mealPhotos)));

    }
    
    [HttpGet]
    public async Task<List<MealResponse>> GetMeals()
    {
        var query = new ListMealQuery();

        var getMealsResult = await _mediator.Send(query);

        List<MealResponse> mealResponses = new ();
        

        foreach (var meal in getMealsResult)
        {
            var prices = _mapper.Map<ICollection<PriceDto>>(meal.Prices);
            var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(meal.MealIngredients);
            var mealPhotos =  _mapper.Map<List<PhotoDto>>(meal.Photos);
            mealResponses.Add(new MealResponse(
                new MealDto(
                    Id:meal.Id,
                    Name:meal.Name.Value,
                    Description:meal.Description,
                    BestSeller:meal.BestSeller,
                    Promotion: meal.Promotion,
                    PromotionRate: meal.PromotionRate,
                    Category: ToDto(meal.Category),
                    Prices : prices,
                    Ingrediants: mealIngredients,
                    Photos: mealPhotos
                )));
        }

        return  mealResponses;

    }

    [HttpDelete("{mealId:guid}")]
    public async Task<ActionResult> DeleteMeal(Guid mealId)
    {
        var query = new DeleteMealCommand(mealId);
        await _mediator.Send(query);
        return NoContent();
    }
    
    private static Category ToDto(DomainCategory category)
    {
        return category.Name switch
        {
            nameof(DomainCategory.Pizza) => Category.Pizza,
            nameof(DomainCategory.Sandwich) => Category.Sandwich,
            nameof(DomainCategory.Coffee) => Category.Coffee,
            _ => throw new InvalidOperationException(),
        };
    }
}