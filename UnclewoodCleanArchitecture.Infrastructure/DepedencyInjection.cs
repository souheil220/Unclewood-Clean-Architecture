using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Ingredient.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPersistence();
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<UnclewoodDbContext>(options =>
            options.UseNpgsql("Host=localhost;Database=luxurysmartphone;Username=luxurysmartphone;Password="));

        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IIngrediantsRepository, IngredientRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UnclewoodDbContext>());

        return services;
    }
}