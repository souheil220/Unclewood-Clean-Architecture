namespace UnclewoodCleanArchitecture.Domain.Exepptions;

public abstract class DomainException<T> : Exception
{
    public T Value { get; }

    protected DomainException(string message, T value) : base(message)
    {
        Value = value;
    }

    protected DomainException(string message) : base(message)
    {
        Value = default;
    }
}