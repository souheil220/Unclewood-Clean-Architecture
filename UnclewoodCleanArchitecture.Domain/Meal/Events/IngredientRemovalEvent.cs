using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Meal.Events;

public record IngredientRemovalEvent(Guid IngridientId):IDomainEvent;