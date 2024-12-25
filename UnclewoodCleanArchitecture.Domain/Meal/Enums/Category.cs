using Ardalis.SmartEnum;

namespace UnclewoodCleanArchitecture.Domain.Meal.Enums;

public sealed class Category  : SmartEnum<Category>
{
    public static readonly Category Pizza = new(nameof(Pizza), 0);
    public static readonly Category Sandwich = new(nameof(Sandwich), 1);
    public static readonly Category Coffee = new(nameof(Coffee), 2);
    public Category(string name, int value) : base(name, value)
    {
    }
}