using System.Net;
using Microsoft.AspNetCore.Http;
using PathPilot.Shared.Abstractions.Exceptions;

namespace PathPilot.Shared.Infrastructure.Exceptions;

internal sealed class ErrorHandlerMiddleware(
    IExceptionCompositionRoot exceptionCompositionRoot)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = exceptionCompositionRoot.Map(exception);
        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;
        if (response is null)
            return;
        await context.Response.WriteAsJsonAsync(response);
    }
}