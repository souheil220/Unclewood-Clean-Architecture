using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Role.Enum;

namespace UnclewoodCleanArchitecture.Infrastructure.RolePermission;

public class RolePermissionConfigurations: IEntityTypeConfiguration<Domain.Common.Entities.RolePermission>
{
    public void Configure(EntityTypeBuilder<Domain.Common.Entities.RolePermission> builder)
    {
        List<Domain.Common.Entities.RolePermission> data = new List<Domain.Common.Entities.RolePermission>
        {
            // Admin Permissions
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.MealDelete.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.MealAdd.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.IngredientAdd.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.IngredientDelete.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.IngredientRead.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.UsersRead.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.UserDelete.Id),
            CreatePermission(Domain.Role.Role.Admin.Id, Domain.Permission.Permission.UserAdd.Id),

            // Manager Permissions
            CreatePermission(Domain.Role.Role.Manager.Id, Domain.Permission.Permission.MealAdd.Id),
            CreatePermission(Domain.Role.Role.Manager.Id, Domain.Permission.Permission.IngredientAdd.Id),
            CreatePermission(Domain.Role.Role.Manager.Id, Domain.Permission.Permission.IngredientRead.Id)
        };

    builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.HasData(data);
    }
    
    // Method to create RolePermission
    Domain.Common.Entities.RolePermission CreatePermission(int roleId, int permissionId)
    {
        return new Domain.Common.Entities.RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
    }
}