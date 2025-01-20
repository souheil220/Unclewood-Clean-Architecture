using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Infrastructure.Ingredient.Persistence;

public class IngredientConfigurations : IEntityTypeConfiguration<UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient>
{
    public void Configure(EntityTypeBuilder<UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.Name)
            .HasConversion(
                name => name.Value, 
                value => Name.Create(value));
        
        builder.OwnsOne(i => i.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Value)
                .IsRequired();
            priceBuilder.Property(p => p.Currency)
                .IsRequired();
        });
        
        builder.OwnsMany(m => m.DisponibleIn, disponibleInBuilder =>
        {
            disponibleInBuilder.WithOwner()
                .HasForeignKey("IngredientId");
            
            disponibleInBuilder.Property(p => p.Value)
                .HasColumnName("DisponibleInValue")
                .IsRequired();
            disponibleInBuilder.Property(p => p.Name)
                .HasColumnName("DisponibleInName")
                .IsRequired();
        });
        
       // builder.OwnsMany(m => m.DisponibleIn);
        
    }
}
