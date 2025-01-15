namespace UnclewoodCleanArchitecture.Infrastructure.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}