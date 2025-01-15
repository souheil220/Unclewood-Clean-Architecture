using FluentValidation;
using UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public class IngredientCommandValidator : AbstractValidator<CreateMealCommand>
{
    public IngredientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 3 and 50 characters");
    }
}