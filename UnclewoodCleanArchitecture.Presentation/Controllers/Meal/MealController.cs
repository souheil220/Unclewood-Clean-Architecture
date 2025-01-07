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
    public async Task<ActionResult> CreateMeal([FromBody] CreateMealRequest mealRequest,CancellationToken concellationToken)
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
        
        var createMealResult = await _mediator.Send(command,concellationToken);

        if (createMealResult.IsFailure)
        {
            return BadRequest(createMealResult.Error);
        }
        
        var prices = _mapper.Map<ICollection<PriceDto>>(createMealResult.Value.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(createMealResult.Value.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(createMealResult.Value.Photos);
        
        return Ok(new MealResponse(
            new MealDto(
                Id:createMealResult.Value.Id,
                Name:createMealResult.Value.Name.Value,
                Description:createMealResult.Value.Description,
                BestSeller:createMealResult.Value.BestSeller,
                Promotion: createMealResult.Value.Promotion,
                PromotionRate: createMealResult.Value.PromotionRate,
                Category: ToDto(createMealResult.Value.Category),
                Prices : prices,
                Ingrediants: mealIngredients,
                Photos: mealPhotos)));
    }
    
    [HttpGet("{mealId:guid}")]
    public async Task<IActionResult> GetMeal(Guid mealId)
    {
        var query = new GetMealQuery(mealId);

        var getMealResult = await _mediator.Send(query);

        if (getMealResult.IsFailure)
        {
            return BadRequest(getMealResult.Error);
        }
        
        var prices = _mapper.Map<ICollection<PriceDto>>(getMealResult.Value.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(getMealResult.Value.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(getMealResult.Value.Photos);

        return Ok(new MealResponse(
            new MealDto(
                Id:getMealResult.Value.Id,
                Name:getMealResult.Value.Name.Value,
                Description:getMealResult.Value.Description,
                BestSeller:getMealResult.Value.BestSeller,
                Promotion: getMealResult.Value.Promotion,
                PromotionRate: getMealResult.Value.PromotionRate,
                Category: ToDto(getMealResult.Value.Category),
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
        

        foreach (var meal in getMealsResult.Value)
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