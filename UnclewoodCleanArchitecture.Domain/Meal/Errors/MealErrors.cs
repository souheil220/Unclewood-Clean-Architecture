using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Domain.Meal.Errors;

public static class MealErrors
{
    public static Error NotFound => new(
        "Meal.Found",
        "The meal with the specified id doesn't exist."
    );
    
    public static Error MealAlreadyExist => new(
        "Meal.Exist",
        "The meal specified already exist."
    );
    
    public static Error PhotoNotFound => new(
        "Meal.Found",
        "The photo with the specified id doesn't exist."
    );
    
    public static Error PhotoAlreadyExist => new(
        "Photo.Exist",
        "The photo with the specified Url doesn't exist."
    );
   
}