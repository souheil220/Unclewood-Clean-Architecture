using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure.Common.Ingredient.Persistence;

public class IngredientConfigurations : IEntityTypeConfiguration<UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient>
{
    public void Configure(EntityTypeBuilder<UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

       /* builder.Property("_maxGyms")
            .HasColumnName("MaxGyms");*/

      /*  builder.Property(s => s.AdminId);

        builder.Property(s => s.SubscriptionType)
            .HasConversion(
                subscriptionType => subscriptionType.Value,
                value => SubscriptionType.FromValue(value));*/

        /*builder.Property<List<Guid>>("_gymIds")
            .HasColumnName("GymIds")
            .HasListOfIdsConverter();*/
    }
}
