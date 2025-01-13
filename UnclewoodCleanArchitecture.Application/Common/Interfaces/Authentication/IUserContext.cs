namespace UnclewoodCleanArchitecture.Application.Common.Interfaces.Authentication;

public interface IUserContext
{
    Guid UserId { get; }

    string IdentityId { get; }
}