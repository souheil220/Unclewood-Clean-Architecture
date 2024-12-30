using Microsoft.AspNetCore.Builder;
using UnclewoodCleanArchitecture.Infrastructure.Common.Middleware;

namespace UnclewoodCleanArchitecture.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();
        return builder;
    }
}