using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UnclewoodCleanArchitecture.Application.Behaviors;
using UnclewoodCleanArchitecture.Application.Helper;

namespace UnclewoodCleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            options.AddOpenBehavior(typeof(QueryCachingBehavior<,>));

        });
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        return services;
    }
}