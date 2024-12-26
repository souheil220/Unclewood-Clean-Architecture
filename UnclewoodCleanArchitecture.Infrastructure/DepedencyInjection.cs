using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnclewoodCleanArchitectur.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitectur.Infrastructure.Meal.Persistence;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;

namespace UnclewoodCleanArchitectur.Infrastructure;

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
            options.UseNpgsql("Host=localhost;Database=UnclewoodCleanArchitectur;Username=souhil;Password=myPassword"));

        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IIngrediantsRepository, IngrediantsRepository>();
        //services.AddScoped<ISubscriptionsRepository, IngredientRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UnclewoodDbContext>());

        return services;
    }
}