using UnclewoodCleanArchitecture.Domain.Common.Events;

namespace UnclewoodCleanArchitecture.Domain.Common.Models;

public class AggregateRoot : Entity
{
    public AggregateRoot(Guid id) : base(id)
    {
    }
    
}