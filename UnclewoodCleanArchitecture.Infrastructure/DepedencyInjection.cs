using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Data;
using UnclewoodCleanArchitecture.Infrastructure.Ingredient.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;

namespace UnclewoodCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration);
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                               throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<UnclewoodDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        
        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IIngrediantsRepository, IngredientRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UnclewoodDbContext>());

        return services;
    }
}