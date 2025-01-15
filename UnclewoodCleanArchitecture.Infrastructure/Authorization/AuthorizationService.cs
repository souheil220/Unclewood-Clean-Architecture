using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Application.Caching;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly UnclewoodDbContext _dbContext;
    private readonly ICacheService _cacheService;


    public AuthorizationService(UnclewoodDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var cacheKey = $"auth:roles-{identityId}";
        var cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is not null)
        {
            return cachedRoles;
        }
        
        var roles = await _dbContext.Set<Domain.User.User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstAsync();
        await _cacheService.SetAsync(cacheKey, roles);
        
        return roles;
    }

   public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
   {
       var cacheKey = $"auth:permissions-{identityId}";
       var cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);
       
       if (cachedPermissions is not null)
       {
           return cachedPermissions;
       }
       
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
       
       var permissionsSet =  permissions.ToHashSet();
       
       await _cacheService.SetAsync(cacheKey, permissionsSet);
       
       return permissionsSet;
   }
}