using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.Errors;
using UnclewoodCleanArchitecture.Domain.Ingredient.Events;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;
using UnclewoodCleanArchitecture.Domain.Meal.Errors;
using UnclewoodCleanArchitecture.Domain.Meal.Events;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Domain.Meal;

public sealed class Meal : AggregateRoot
{
    public Meal( 
        Name name, 
        string description, 
        bool bestSeller, 
        bool promotion, 
        double promotionRate, 
        Category category,Guid? id = null) : base(id?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        BestSeller = bestSeller;
        Promotion = promotion;
        PromotionRate = promotionRate;
        Category = category;
    }
    
    public Name Name { get; private set; }
    
    public List<Price> Prices { get; private set; } = new();
    
    public string Description { get; private set; }
   
    public bool BestSeller { get; private set; } = false;
    
    public bool Promotion { get; private set; }
    
    public double PromotionRate { get; private set; } = 0;
  
    public Category Category { get; private set; } = null!;
    
   public List<MealIngredient> MealIngredients { get; private set; } = new();
    
   public List<Photo> Photos { get; private set; } = new();
    
    private Result AddIngredient(Guid ingredientId)
    {
        if (MealIngredients.Any(mi => mi.IngredientId == ingredientId))
        {
            return Result.Failure(IngredientErrors.IngredientAlreadyExist);
        }
        
        MealIngredients.Add(new MealIngredient(Id, ingredientId));
        return Result.Success();
    }

    public void AddIngredients(IEnumerable<Guid> ingredientIds)
    {
        foreach (var ingredientId in ingredientIds)
        {
            AddIngredient(ingredientId);
        }
    }

    public Result RemoveIngredient(Guid ingredientId)
    {
        var ingredient = MealIngredients.FirstOrDefault(mi => mi.IngredientId == ingredientId);
        
        if (ingredient is null)
        {
         return Result.Failure(IngredientErrors.IngredientNotFound);
        }
        MealIngredients.Remove(ingredient);
        
        _domainEvents.Add(new IngredientRemovalEvent(ingredientId));

        return Result.Success();
    }

    private void AddPrice(Price price)
    {
        Prices.Add(new Price(price.Value,price.Currency,price.Location));
    }

    public void AddPrices(ICollection<Price> prices)
    {
        foreach (var price in prices)
        {
            AddPrice(price);
        }
    }
    private Result AddPhoto(Photo photo)
    {
        if (Photos.Any(ph => ph.Url == photo.Url))
        {
            return Result.Failure(MealErrors.PhotoAlreadyExist);
        }
        
        Photos.Add(Photo.Create(photo.Url,"",photo.Name,""));
        return Result.Success();
    }
    
    public void AddPhotos(List<Photo> mealPhotos)
    {
        foreach (var mealPhoto in mealPhotos)
        {
            AddPhoto(mealPhoto);
        }
    }

    public Result RemovePhoto(Guid photoId)
    {
        var photo = Photos.FirstOrDefault(ph => ph.Id == photoId);
        
        if (photo is null)
        {
            return Result.Failure(MealErrors.PhotoNotFound);
        }

        Photos.Remove(photo);

        return Result.Success();
    }

    public void RaiseMealsListed()
    {
        _domainEvents.Add(new MealListedEvent(Id));
    }
    public void RaiseMealCreatedEvent()
    {
        _domainEvents.Add(new MealCreatedEvent(Id));
    }
    
    private Meal():base(Guid.NewGuid()){}
}