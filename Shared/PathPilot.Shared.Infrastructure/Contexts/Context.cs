using Microsoft.AspNetCore.Http;
using PathPilot.Shared.Abstractions.Contexts;

namespace PathPilot.Shared.Infrastructure.Contexts;

public class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; }
    public IIdentityContext Identity { get; }

    public Context(HttpContext context) : this(context.TraceIdentifier, new IdentityContext(context.User))
    {
        
    }

    internal Context(string traceId, IIdentityContext identity)
    {
        TraceId = traceId;
        Identity = identity;
    }
    
    private Context(){}

    public static IContext Empty => new Context();
}