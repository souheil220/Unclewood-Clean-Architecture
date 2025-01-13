using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Users.Queries.ListUsers;

public record ListUsersQuery : IQuery<List<Domain.User.User>>;