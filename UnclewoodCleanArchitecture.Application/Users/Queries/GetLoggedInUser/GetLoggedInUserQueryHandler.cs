using Dapper;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Authentication;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Users.Queries.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler
    : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetLoggedInUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = $"""
                            SELECT
                                 "Id",
                               "FirstName",
                               "LastName",
                               "Email"
                            FROM "Users"
                            WHERE "IdentityId" = @IdentityId
                            """;

        var user = await connection.QuerySingleAsync<UserResponse>(
            sql,
            new
            {
                _userContext.IdentityId
            });

        return user;
    }
}