using PathPilot.Shared.Abstractions.Contexts;

namespace PathPilot.Shared.Infrastructure.Contexts;

public interface IContextFactory
{
    IContext Create();
}