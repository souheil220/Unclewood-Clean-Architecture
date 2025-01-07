using FluentValidation;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public class MealCommandValidator : AbstractValidator<CreateMealCommand>
{
    public MealCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 3 and 50 characters");
    }
}