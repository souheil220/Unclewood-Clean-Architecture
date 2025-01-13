using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username,CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid guid,CancellationToken cancellationToken = default);
    
    //Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
    void AddUser (User user);
    void DeleteUser(User user);
}