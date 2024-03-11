using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlerMiddleware>();
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();
        
        return services;
    }

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        return app;
    }
}