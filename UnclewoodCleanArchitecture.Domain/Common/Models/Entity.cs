using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Common.Models;

public abstract class Entity
{
    public Guid Id { get; init; }
    protected readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id)
    {
        Id = id;
    }
    protected Entity(){}

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();
        return copy;
    }
    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType() )
        {
            return false;
        }

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    
}