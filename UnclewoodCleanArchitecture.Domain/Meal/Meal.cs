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
    private Meal( 
        Name name, 
        Descriptiion description, 
        BestSeller bestSeller, 
        Promotion promotion, 
        PromotionRate promotionRate, 
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
    
    public Descriptiion Description { get; private set; }
   
    public BestSeller BestSeller { get; private set; }
    
    public Promotion Promotion { get; private set; }
    
    public PromotionRate PromotionRate { get; private set; }
  
    public Category Category { get; private set; } = null!;
    
   public List<MealIngredient> MealIngredients { get; private set; } = new();
    
   public List<Photo> Photos { get; private set; } = new();

   public readonly List<Price> NewPrices = new();
   
   
   public static Meal Create( 
       Name name, 
       Descriptiion description, 
       BestSeller bestSeller, 
       Promotion promotion, 
       PromotionRate promotionRate, 
       Category category)
   {
       var meal = new Meal(name,
           description,
           bestSeller,
           promotion,
           promotionRate,
           category);
     
       /* If I had a chain reaction for example that it should happen after the addition of a new mela
      I would raise the events just like a did below */
       meal.RaiseMealCreatedEvent();

       return meal;
   } 
   
   
   
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
    
    // Method for applying the promotion (business logic in the domain)
    public void ApplyPromotionIfNecessary(List<Price> prices, bool hasPromotion, decimal promotionRate)
    {
        if (hasPromotion)
        {
            foreach (var price in prices)
            {
                NewPrices.Add(new Price(price.ApplyDiscount(promotionRate).Value.Value, price.Currency, price.Location));
            }

        }
    }

    public void AddPrices(ICollection<Price> prices)
    {
        foreach (var price in prices)
        {
            AddPrice(price);
        }
    }
    private void AddPhoto(Photo photo)
    {
        if (Photos.Any(ph => ph.Url == photo.Url))
        {
            Result.Failure(MealErrors.PhotoAlreadyExist);
            return;
        }
        
        Photos.Add(Photo.Create(photo.Url,"",photo.Name,""));
        Result.Success();
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

    private void RaiseMealCreatedEvent()
    {
        _domainEvents.Add(new MealCreatedEvent(Id));
    }
    
    private Meal():base(Guid.NewGuid()){}
}