using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.User.Events;

public record UserCreatedEvent(Guid User):IDomainEvent;