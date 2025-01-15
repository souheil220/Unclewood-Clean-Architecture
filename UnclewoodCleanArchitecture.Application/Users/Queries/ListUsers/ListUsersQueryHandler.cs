using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Application.Users.Queries.ListUsers;

public class ListUsersQueryHandler(IUserRepository userRepository,ISqlConnectionFactory mealConnectionFactory)
    : IQueryHandler<ListUsersQuery, List<User>>
{

    

    public async Task<Result<List<User>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllUsersAsync(cancellationToken);
        
        return Result.Success(users);
    }
}