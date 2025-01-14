using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;

namespace UnclewoodCleanArchitecture.Application.Users.Queries.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;