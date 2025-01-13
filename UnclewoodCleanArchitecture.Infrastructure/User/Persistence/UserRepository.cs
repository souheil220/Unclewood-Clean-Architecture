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

    public async void AddUser(Domain.User.User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
    

    public  void DeleteUser(Domain.User.User user)
    {
        _dbContext.Users.Remove(user);
    }
}