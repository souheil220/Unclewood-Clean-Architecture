using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.User.Persistence;

public class UserRepository : IUserRepository
{
    private readonly UnclewoodDbContext _dbContext;

    public UserRepository(UnclewoodDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.User.User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .Where(x => x.FirstName == username)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<Domain.User.User?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
    {
       return await _dbContext.Users
           .FirstOrDefaultAsync(x => x.Id == guid, cancellationToken);
    }

    public async Task<List<Domain.User.User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.ToListAsync(cancellationToken);
    }

    public async Task AddUser(Domain.User.User user)
    {
        foreach (var role in user.Roles)
        {
            _dbContext.Attach(role);
        }
        await _dbContext.Users.AddAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        _dbContext.Users.Remove(user);
    }
}