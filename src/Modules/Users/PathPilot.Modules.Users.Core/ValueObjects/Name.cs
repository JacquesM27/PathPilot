using PathPilot.Modules.Users.Core.Exceptions;

namespace PathPilot.Modules.Users.Core.ValueObjects;

public sealed record Name
{
    public string FirstName { get; }
    public string LastName { get; }

    public Name(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new EmptyFirstNameException();

        if (string.IsNullOrWhiteSpace(lastName))
            throw new EmptyLastNameException();
        
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
        => FirstName + " " + LastName;
}