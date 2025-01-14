using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Role.Enum;
using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Infrastructure.Role.Persistence;

public class RoleConfigurations: IEntityTypeConfiguration<Domain.Role.Role>
{
    public void Configure(EntityTypeBuilder<Domain.Role.Role> builder)
    {
        builder.HasKey(role => role.Id);
        
        builder.Property(m => m.Name)
            .HasConversion(
                name => name.Value, 
                value => Name.Create(value));
        
        builder.HasMany(role => role.Users)
            .WithMany(user => user.Roles);

        builder.HasData(Domain.Role.Role.Manager);
        builder.HasData(Domain.Role.Role.Admin);
    }
}