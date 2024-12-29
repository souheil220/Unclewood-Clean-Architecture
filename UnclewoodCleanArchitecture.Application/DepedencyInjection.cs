using Microsoft.Extensions.DependencyInjection;
using UnclewoodCleanArchitecture.Application.Helper;

namespace UnclewoodCleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        return services;
    }
}