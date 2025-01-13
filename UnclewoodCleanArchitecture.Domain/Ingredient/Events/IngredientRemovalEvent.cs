using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Ingredient.Events;

public record IngredientRemovalEvent(Guid IngridientId):IDomainEvent;