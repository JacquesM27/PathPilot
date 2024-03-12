namespace PathPilot.Shared.Abstractions.Commands;

public abstract record Command : ICommand
{
    public string CommandId { get; init; }

    protected Command()
    {
        CommandId = $"{GetType().Name}/{Guid.NewGuid()}";
    }
}