using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Meal.Enums;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;

public class MealConfigurations : IEntityTypeConfiguration<Domain.Meal.Meal>
{
    public void Configure(EntityTypeBuilder<Domain.Meal.Meal> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        
        builder.Property(m => m.Name)
            .HasConversion(
                name => name.Value, 
                value => Name.Create(value));
        
        builder.Property(m => m.Description)
            .HasConversion(
                description => description.Value, 
                value => Descriptiion.Create(value));
        
        builder.Property(m => m.BestSeller)
            .HasConversion(
                bestSeller => bestSeller.Value, 
                value => new BestSeller(value));
        
        builder.Property(m => m.Promotion)
            .HasConversion(
                promotion => promotion.Value, 
                value => new Promotion(value));
        
        builder.Property(m => m.PromotionRate)
            .HasConversion(
                promotionRate => promotionRate.Value, 
                value => PromotionRate.Create(value));
        
        builder.Property(m => m.Category)
            .HasConversion(
                category => category.Value, 
                value => Category.FromValue(value));
        
        

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
