using Microsoft.AspNetCore.Http;
using PathPilot.Shared.Abstractions.Contexts;

namespace PathPilot.Shared.Infrastructure.Contexts;

internal sealed class ContextFactory(IHttpContextAccessor httpContextAccessor) : IContextFactory
{
    public IContext Create()
    {
        var httpContext = httpContextAccessor.HttpContext;
        return httpContext is null ? Context.Empty : new Context(httpContext);
    }
}