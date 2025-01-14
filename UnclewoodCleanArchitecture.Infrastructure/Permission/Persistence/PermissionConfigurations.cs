using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;

namespace UnclewoodCleanArchitecture.Infrastructure.Permission.Persistence;

public class PermissionConfigurations: IEntityTypeConfiguration<Domain.Permission.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Permission.Permission> builder)
    {
        builder.HasKey(permission => permission.Id);
        
        builder.Property(m => m.Name)
            .HasConversion(
                name => name.Value, 
                value => Name.Create(value));


        builder.HasData(Domain.Permission.Permission.MealDelete);
        builder.HasData(Domain.Permission.Permission.MealAdd);
        builder.HasData(Domain.Permission.Permission.IngredientRead);
        builder.HasData(Domain.Permission.Permission.IngredientDelete);
        builder.HasData(Domain.Permission.Permission.IngredientAdd);
        builder.HasData(Domain.Permission.Permission.UsersRead);
        builder.HasData(Domain.Permission.Permission.UserDelete);
        builder.HasData(Domain.Permission.Permission.UserAdd);
    }

   
}