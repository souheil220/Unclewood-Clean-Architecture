using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;
namespace UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;

public class MealConfigurations : IEntityTypeConfiguration<Domain.Meal.Meal>
{
    public void Configure(EntityTypeBuilder<Domain.Meal.Meal> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();
       
        builder.Property(m => m.Category)
            .HasConversion(
                category => category.Value, 
                value => Category.FromValue(value));
        
        builder.Property(m => m.Name)
            .HasConversion(
                name => name.Value, 
                value => Name.Create(value));

        builder.OwnsMany(m => m.Prices, priceBuilder =>
        {
            priceBuilder.WithOwner().HasForeignKey("MealId");
            
            priceBuilder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("Value")
                .HasColumnType("decimal(18,2)");
            
            priceBuilder.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(3) // Assuming currency is a 3-letter code
                .HasColumnName("Currency");

            priceBuilder.Property(p => p.Location)
                .HasColumnName("Location")
                .HasConversion(
                    location => location.Value,
                    value => Location.FromValue(value));

        });
        
        // Configure Photos collection
        builder.OwnsMany(m => m.Photos, photoBuilder =>
        {
            photoBuilder.WithOwner()
                .HasForeignKey("MealId");
            photoBuilder.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(2048); // Enforce URL length limit
        });
    }
    

   
}
