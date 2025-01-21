using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.Meal;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;
using UnclewoodCleanArchitecture.Application.Meal.Commands.DeleteMeal;
using UnclewoodCleanArchitecture.Application.Meal.Commands.UpdateMeal;
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
            mealRequest.MealPictures,
            mealRequest.IngrediantsIDs,
            mealRequest.Category, 
            BestSeller: mealRequest.BestSeller, 
            Promotion: mealRequest.Promotion, 
            PromotionRate: mealRequest.PromotionRate);
        
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
                Description:createMealResult.Value.Description.Value,
                BestSeller:createMealResult.Value.BestSeller.Value,
                Promotion: createMealResult.Value.Promotion.Value,
                PromotionRate: createMealResult.Value.PromotionRate.Value,
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
        
        return Ok(getMealResult.Value);

    }

    [HttpPatch("{mealId:guid}")]
    [HasPermission(Permissions.MealAdd)]
    public async Task<ActionResult> UpdateMeal(UpdateMealCommand mealCommand,Guid mealId,CancellationToken cancellationToken)
    {
        var command = mealCommand with { MealId = mealId };
        var response = await _mediator.Send(command,cancellationToken);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }
        return Ok(response.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMeals()
    {
        var query = new ListMealQuery();

        var getMealsResult = await _mediator.Send(query);
        

        return  Ok(getMealsResult.Value);

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