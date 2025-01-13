using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Infrastructure.User.Persistence;

public class UserConfigurations: IEntityTypeConfiguration<Domain.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.User.User> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedNever();
        
        builder.Property(m => m.FirstName)
            .HasConversion(
                firstName => firstName.Value, 
                value => Name.Create(value));
        
        builder.Property(m => m.LastName)
            .HasConversion(
                lastName => lastName.Value, 
                value => Name.Create(value));
        
        builder.Property(m => m.Email)
            .HasConversion(
                email =>  email.Value,
                value => new Email(value));
        
        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.IdentityId).IsUnique();
        
    }
}