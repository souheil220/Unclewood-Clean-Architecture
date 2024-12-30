using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Meal.Events;

public record MealCreatedEvent(Guid MealId):IDomainEvent;