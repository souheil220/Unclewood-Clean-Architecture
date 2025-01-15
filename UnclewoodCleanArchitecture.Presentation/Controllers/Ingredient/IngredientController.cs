using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.Ingredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;
using UnclewoodCleanArchitecture.Infrastructure.Authorization;
using DomainLocation = UnclewoodCleanArchitecture.Domain.Common.Enum.Location;

namespace UnclewoodCleanArchitectur.Presentation.Controllers.Ingredient;

[Authorize]
public class IngredientController : BaseApiController
{
    private readonly ISender _mediator;

    public IngredientController(ISender mediator)
    {
        _mediator = mediator;
    }
   
    [HttpPost]
    [HasPermission(Permissions.IngredientAdd)]
    public async Task<ActionResult> CreateIngrediant([FromBody] CreateIngridientRequest ingrediantRequest)
    {
        var command = new CreateIngredientCommand(
            ingrediantRequest.Name,
            ingrediantRequest.DisponibleIn,
            ingrediantRequest.PriceValue,
            ingrediantRequest.PriceCurrency
            );
        
        var createIngredientResult = await _mediator.Send(command);

        if (createIngredientResult.IsFailure)
        {
            return BadRequest(createIngredientResult.Error);
        }

        return Ok(new IngredientResponse(

            createIngredientResult.Value.Id,
            createIngredientResult.Value.Name,
            ingrediantRequest.DisponibleIn,
            createIngredientResult.Value.Price.Value));
    }
    

    [HttpGet("{ingredientId:guid}")]
    [HasPermission(Permissions.IngredientRead)]
    public async Task<IActionResult> GetIngredient(Guid ingredientId)
    {
        var query = new GetIngredientQuery(ingredientId);

        var getIngredientsResult = await _mediator.Send(query);

        if (getIngredientsResult.IsFailure)
        {
            return BadRequest(getIngredientsResult.Error);
        }

        return Ok(new IngredientResponse(
                getIngredientsResult.Value.Id,
                getIngredientsResult.Value.Name,
                getIngredientsResult.Value.DisponibleIn,
               // ToDto(getIngredientsResult.Value.DisponibleIn),
                getIngredientsResult.Value.Price));

    }
    
    [HttpGet]
    [HasPermission(Permissions.IngredientRead)]
    public async Task<List<IngredientResponse>> GetIngredients()
    {
        var query = new ListIngredientQuery();

        var getIngredientsResult = await _mediator.Send(query);

       List<IngredientResponse> ingredientResponses = new ();

       foreach (var ingredient in getIngredientsResult.Value)
       {
           ingredientResponses.Add(new IngredientResponse(
                                                    ingredient.Id, 
                                                    ingredient.Name, 
                                                    ToDto(ingredient.DisponibleIn),
                                                    ingredient.Price.Value 
                                                    ));
       }
        return  ingredientResponses;
    }
    
    [HttpDelete("{ingredientId:guid}")]
    [HasPermission(Permissions.IngredientDelete)]
    public async Task<IActionResult> DeleteIngredient(Guid ingredientId)
    {
        var query = new DeleteIngredientCommand(ingredientId);
        await _mediator.Send(query);
        return NoContent();
    }
    

    private static List<string> ToDto(List<DomainLocation> locations)
    {
        List<string> result = new();
        foreach (var location in locations)
        {
            result.Add(location.Name);
        }

        return result;

    }
}