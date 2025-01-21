using FluentValidation;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public class MealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public MealCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 3 and 50 characters");
        RuleFor(x => x.Prices).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Prices).Must(x => x.Count != 0).WithMessage("Meal must have at least one Price");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Description).Length(5, 500).WithMessage("Description must be between 5 and 500 characters");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.MealPictures).NotEmpty().WithMessage("Pictures is required");
        RuleFor(x => x.MealPictures).Must(x => x.Count != 0).WithMessage("Meal must have at least one Picture");
        RuleFor(x => x.IngrediantsIDs).NotEmpty().WithMessage("Ingredients is required");
        RuleFor(x => x.IngrediantsIDs).Must(x => x.Count != 0).WithMessage("Meal must have at least one Ingredient");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Category).Must(x => x is "Pizza" or "Coffee" or "Sandwich").WithMessage("Category must be either  Pizza,Coffee or Sandwich");
    }
}