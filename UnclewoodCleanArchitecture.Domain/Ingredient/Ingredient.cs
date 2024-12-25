using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects;

namespace UnclewoodCleanArchitecture.Domain.Ingredient;

public sealed class Ingredient : AggregateRoot
{
    public Ingredient(
        Name name, 
        Price price,Guid? id=null) : base(id??Guid.NewGuid())
    {
        Name = name;
        Price = price;
    }
    public Name Name { get; private set; }

    
    public List<Location> DisponibleIn { get; private set; } = new List<Location>();
    
    public Price Price { get; private set; }
   
    // Navigation property for the join table
   public List<MealIngredient> MealIngrediants { get; set; } = new List<MealIngredient>();
}