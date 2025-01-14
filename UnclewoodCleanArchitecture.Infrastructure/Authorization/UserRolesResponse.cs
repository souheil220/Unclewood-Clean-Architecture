namespace UnclewoodCleanArchitecture.Infrastructure.Authorization;

public class UserRolesResponse
{
    public Guid UserId { get; init; }

    public List<Domain.Role.Role> Roles { get; init; } = [];
}