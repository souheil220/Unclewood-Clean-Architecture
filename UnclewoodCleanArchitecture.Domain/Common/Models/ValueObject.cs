namespace UnclewoodCleanArchitecture.Domain.Common.Models;

public  abstract class ValueObject
{
    public abstract IEnumerable<object?> GetEqualityComponent();

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }
        return ((ValueObject)obj).GetEqualityComponent()
            .SequenceEqual(GetEqualityComponent());
    }

    public override int GetHashCode()
    {
        
        return GetEqualityComponent()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate(0, (current, obj) => current ^ obj.GetHashCode());
    }
}