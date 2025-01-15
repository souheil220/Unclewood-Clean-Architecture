using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.Meal;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;
using UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;
using UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;
using UnclewoodCleanArchitecture.Infrastructure.Authorization;

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
    [HasPermission(Permissions.MealAdd)]
    public async Task<ActionResult> CreateMeal([FromBody] CreateMealRequest mealRequest,CancellationToken concellationToken)
    {

        var command = new CreateMealCommand(
            mealRequest.Name,
            mealRequest.Prices,
            mealRequest.Description,
            mealRequest.BestSeller,
            mealRequest.Promotion,
            mealRequest.PromotionRate,
            mealRequest.MealPictures,
            mealRequest.IngrediantsIDs,
            mealRequest.Category);
        
        var createMealResult = await _mediator.Send(command,concellationToken);

        if (createMealResult.IsFailure)
        {
            return BadRequest(createMealResult.Error);
        }
        
        var prices = _mapper.Map<ICollection<PriceDto>>(createMealResult.Value.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(createMealResult.Value.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(createMealResult.Value.Photos);
        
        return Ok(new MealResponse(
       
                Id:createMealResult.Value.Id,
                Name:createMealResult.Value.Name.Value,
                Description:createMealResult.Value.Description,
                BestSeller:createMealResult.Value.BestSeller,
                Promotion: createMealResult.Value.Promotion,
                PromotionRate: createMealResult.Value.PromotionRate,
                Category: createMealResult.Value.Category.Name,
                Prices : prices,
                Ingrediants: mealIngredients,
                Photos: mealPhotos));
    }
    
    [HttpGet("{mealId:guid}")]
    public async Task<IActionResult> GetMeal(Guid mealId)
    {
        var query = new GetMealQuery(mealId);

        var getMealResult = await _mediator.Send(query);

        if (getMealResult.IsFailure)
        {
            return NotFound(getMealResult.Error);
        }
        
        var prices = _mapper.Map<ICollection<PriceDto>>(getMealResult.Value.Prices);
        var mealIngredients =  _mapper.Map<List<MealIngredientDto>>(getMealResult.Value.MealIngredients);
        var mealPhotos =  _mapper.Map<List<PhotoDto>>(getMealResult.Value.Photos);

        return Ok(new MealResponse(
          
                Id:getMealResult.Value.Id,
                Name:getMealResult.Value.Name.Value,
                Description:getMealResult.Value.Description,
                BestSeller:getMealResult.Value.BestSeller,
                Promotion: getMealResult.Value.Promotion,
                PromotionRate: getMealResult.Value.PromotionRate,
                Category: getMealResult.Value.Category.Name,
                Prices : prices,
                Ingrediants: mealIngredients,
                Photos: mealPhotos));

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
                    Id:meal.Id,
                    Name:meal.Name.Value,
                    Description:meal.Description,
                    BestSeller:meal.BestSeller,
                    Promotion: meal.Promotion,
                    PromotionRate: meal.PromotionRate,
                    Category: meal.Category.Name,
                    Prices : prices,
                    Ingrediants: mealIngredients,
                    Photos: mealPhotos
                ));
        }

        return  mealResponses;

    }
    
    [HasPermission(Permissions.MealDelete)]
    [HttpDelete("{mealId:guid}")]
    public async Task<ActionResult> DeleteMeal(Guid mealId)
    {
        var query = new DeleteMealCommand(mealId);
        //Change it so that you can have a response to handle the not found
        await _mediator.Send(query);
        return NoContent();
    }
    
}