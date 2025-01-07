using MediatR;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.Common.Enums;
using UnclewoodCleanArchitectur.Presentation.DTOs;
using UnclewoodCleanArchitectur.Presentation.Ingredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Commands.DeleteIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Queries.GetIngredient;
using UnclewoodCleanArchitecture.Application.Ingredient.Queries.ListIngredient;
using DomainLocation = UnclewoodCleanArchitecture.Domain.Common.Enum.Location;

namespace UnclewoodCleanArchitectur.Presentation.Controllers.Ingredient;

public class IngredientController : BaseApiController
{
    private readonly ISender _mediator;

    public IngredientController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateIngrediant([FromBody] CreateIngridientRequest ingrediantRequest)
    {
        var command = new CreateIngredientCommand(
            ingrediantRequest.Name.Value,
            EnumConverter(ingrediantRequest.DisponibleIn),
            ingrediantRequest.Price);
        
        var createIngredientResult = await _mediator.Send(command);
        
        return Ok(new IngredientResponse(
            new IngredientDto(
                createIngredientResult.Value.Id,
                createIngredientResult.Value.Name,
                ToDto(createIngredientResult.Value.DisponibleIn),
                createIngredientResult.Value.Price.Value)));
        
        
    }

    [HttpGet("{ingredientId:guid}")]
    public async Task<IActionResult> GetIngredient(Guid ingredientId)
    {
        var query = new GetIngredientQuery(ingredientId);

        var getIngredientsResult = await _mediator.Send(query);

        return Ok(new IngredientResponse(
            new IngredientDto(
                getIngredientsResult.Value.Id,
                getIngredientsResult.Value.Name,
                ToDto(getIngredientsResult.Value.DisponibleIn),
                getIngredientsResult.Value.Price.Value)));

    }
    
    [HttpGet]
    public async Task<List<IngredientResponse>> GetIngredients()
    {
        var query = new ListIngredientQuery();

        var getIngredientsResult = await _mediator.Send(query);

       List<IngredientResponse> ingredientResponses = new ();

       foreach (var ingredient in getIngredientsResult.Value)
       {
           ingredientResponses.Add(new IngredientResponse(
                                    new IngredientDto(ingredient.Id, 
                                                    ingredient.Name, 
                                                    ToDto(ingredient.DisponibleIn),
                                                    ingredient.Price.Value 
                                                    )));
       }

        return  ingredientResponses;

    }

    [HttpDelete("{ingredientId:guid}")]
    public async Task<IActionResult> DeleteIngredient(Guid ingredientId)
    {
        var query = new DeleteIngredientCommand(ingredientId);
        await _mediator.Send(query);
        return NoContent();
    }
    private static List<DomainLocation> EnumConverter(List<DomainLocation> locations)
    {
        List<DomainLocation> result = new();
        foreach (var location in locations)
        {
            DomainLocation.TryFromName(
                location.ToString(),
                out var newLocation);
            result.Add(newLocation);
        }
        return result;
    }

    private static List<Location> ToDto(List<DomainLocation> locations)
    {
        List<Location> result = new();
        foreach (var location in locations)
        {
            var mappedLocation = location.Name switch
            {
                nameof(DomainLocation.SBA) => Location.SBA,
                nameof(DomainLocation.ORAN) => Location.ORAN,
                _ => throw new InvalidOperationException($"Unknown location: {location.Name}")
            };
            result.Add(mappedLocation);
        }

        return result;

    }
}