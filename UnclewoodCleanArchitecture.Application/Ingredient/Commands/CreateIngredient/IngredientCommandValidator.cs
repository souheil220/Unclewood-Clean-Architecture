using FluentValidation;

namespace UnclewoodCleanArchitecture.Application.Ingredient.Commands.CreateIngredient;

public class IngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public IngredientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).Length(2, 50).WithMessage("Name must be between 3 and 50 characters");
        RuleFor(x => x.PriceValue).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.PriceValue).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.PriceCurrency).NotEmpty().WithMessage("Price currency is required");
        RuleFor(x => x.PriceCurrency).Length(2, 50).WithMessage("Name must be between 3 and 50 characters");
        RuleFor(x => x.DisponibleIn).NotEmpty().WithMessage("Disponible in is required");
        RuleFor(x => x.DisponibleIn).Must(x => x.Count != 0).WithMessage("Disponible must have elements");

        
    }
}