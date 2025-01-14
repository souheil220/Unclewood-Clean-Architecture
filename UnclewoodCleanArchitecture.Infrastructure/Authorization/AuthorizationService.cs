using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly UnclewoodDbContext _dbContext;

    public AuthorizationService(UnclewoodDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var roles = await _dbContext.Set<Domain.User.User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstAsync();

        return roles;
    }

   public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
   {
       var permissions = await _dbContext.Set<Domain.User.User>()
           .Where(u => u.IdentityId == identityId)
           .SelectMany(u => u.Roles
               .Join(_dbContext.Set<Domain.Common.Entities.RolePermission>(),
                   role => role.Id,
                   rolePermission => rolePermission.RoleId,
                   (role, rolePermission) => new { role, rolePermission })
               .Join(_dbContext.Set<Domain.Permission.Permission>(),
                   rp => rp.rolePermission.PermissionId,
                   permission => permission.Id,
                   (rp, permission) => permission.Name.Value)
           )
           .ToListAsync();
       return permissions.ToHashSet();
    }
}