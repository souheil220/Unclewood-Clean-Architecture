using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;
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
    
    private void AddIngredient(Guid ingredientId)
    {
        if (MealIngredients.Any(mi => mi.IngredientId == ingredientId))
        {
            //TODO throw new DomainException("This ingredient is already added to the meal");
        }
        
        MealIngredients.Add(new MealIngredient(Id, ingredientId));
    }

    public void AddIngredients(IEnumerable<Guid> ingredientIds)
    {
        foreach (var ingredientId in ingredientIds)
        {
            AddIngredient(ingredientId);
        }
    }

    public void RemoveIngredient(Guid ingredientId)
    {
        var ingredient = MealIngredients.FirstOrDefault(mi => mi.IngredientId == ingredientId);
        
        if (ingredient is null)
        {
         //TODO   throw new DomainException("Ingredient not found in this meal");
        }

        MealIngredients.Remove(ingredient);
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
    private void AddPhoto(Photo photo)
    {
        if (Photos.Any(ph => ph.Url == photo.Url))
        {
            //TODO throw new DomainException("This ingredient is already added to the meal");
        }
        
        Photos.Add(Photo.Create(photo.Url,"",photo.Name,""));
    }
    
    public void AddPhotos(List<Photo> mealPhotos)
    {
        foreach (var mealPhoto in mealPhotos)
        {
            AddPhoto(mealPhoto);
        }
    }
    
    private Meal() : base(id:Guid.NewGuid())
    {}
}