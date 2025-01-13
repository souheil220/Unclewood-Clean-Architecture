using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Authentication;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Infrastructure.Authentication;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Data;
using UnclewoodCleanArchitecture.Infrastructure.Ingredient.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.Meal.Persistence;
using UnclewoodCleanArchitecture.Infrastructure.User.Persistence;
using AuthenticationOptions = UnclewoodCleanArchitecture.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = UnclewoodCleanArchitecture.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = UnclewoodCleanArchitecture.Application.Common.Interfaces.Authentication.IAuthenticationService;

namespace UnclewoodCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
            AddPersistence(services, configuration);
            AddAuthentication(services, configuration);
            return services;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Add these events for debugging
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        Console.WriteLine($"Token received: {context.Token}");
                        return Task.CompletedTask;
                    }
                };
            });
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();
       
        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });
    }

    public static void AddPersistence(IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                               throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<UnclewoodDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        
        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IIngrediantsRepository, IngredientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UnclewoodDbContext>());

    }
}