using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Meal.Events;

public record MealListedEvent(Guid MealId):IDomainEvent;