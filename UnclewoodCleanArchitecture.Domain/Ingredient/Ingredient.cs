using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Domain.Ingredient;

public sealed class Ingredient : AggregateRoot
{
    [JsonConstructor]
    public Ingredient(
        Name name, 
        List<Location> disponibleIn,
        Price price,
        Guid? id=null) : base(id??Guid.NewGuid())
    {
        Name = name;
        DisponibleIn = disponibleIn;
        Price = price;
    }
    public Name Name { get; private set; }
    
    public List<Location> DisponibleIn { get; private set; } = new();
    
    public Price Price { get; private set; }
   
   public List<MealIngredient> MealIngrediants { get; set; } = new();
   
   // Method to update the name
   public void UpdateName(string newName)
   {
       // Optionally, add validation for newName here
       Name = Name.Create(newName);
   }

   public void UpdatePrice(decimal newPrice , string priceCurrency)
   {
       Price = new Price(newPrice, priceCurrency);
   }
   
   public void UpdateLocation(List<string> locations)
   {
       DisponibleIn.Clear();
       foreach (var finalLocation in locations.Select(location => Location.FromName(location)))
       {
           DisponibleIn.Add(finalLocation);
       }
   }
   
   private Ingredient() : base(id:Guid.NewGuid())
   {}
}