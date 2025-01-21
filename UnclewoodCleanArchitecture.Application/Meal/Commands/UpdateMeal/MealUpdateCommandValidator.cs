using FluentValidation;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.UpdateMeal;

public class MealUpdateCommandValidator : AbstractValidator<CreateMealCommand>
{
    public MealUpdateCommandValidator()
    {
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 3 and 50 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));
        RuleFor(x => x.Prices).Must(x => x.Count != 0).WithMessage("Meal must have at least one Price")  
            .When(x => x is not null);
        RuleFor(x => x.Description).Length(5, 500).WithMessage("Description must be between 5 and 500 characters")
            .When(x => x is not null);
        RuleFor(x => x.MealPictures).Must(x => x.Count != 0).WithMessage("Meal must have at least one Picture")
            .When(x => x is not null);
        RuleFor(x => x.IngrediantsIDs).Must(x => x.Count != 0).WithMessage("Meal must have at least one Ingredient")
            .When(x =>x is not null);
        RuleFor(x => x.Category).Must(x => x is "Pizza" or "Coffee" or "Sandwich").WithMessage("Category must be either  Pizza,Coffee or Sandwich")
            .When(x => !string.IsNullOrWhiteSpace(x.Category));
    }
}