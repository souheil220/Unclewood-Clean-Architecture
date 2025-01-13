using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username,CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid guid,CancellationToken cancellationToken = default);
    
    //Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
    
    Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task AddUser (User user);
    Task DeleteUserAsync(Guid userId);
}