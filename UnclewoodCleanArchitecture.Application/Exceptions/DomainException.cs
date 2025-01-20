namespace UnclewoodCleanArchitecture.Application.Exceptions;

public class DomainException:Exception
{
    public DomainException(List<DomainError> errors)
    {
        Errors = errors;
    }

    public List<DomainError> Errors { get;  }
}