using Microsoft.AspNetCore.Builder;
using UnclewoodCleanArchitectur.Presentation.Middleware;

namespace UnclewoodCleanArchitectur.Presentation.Extention;

public static class  ApplicationBuilderExtention
{
    public static IApplicationBuilder AddPresentationMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionhandlingMidlleware>();
        return builder;
    }
}