namespace PathPilot.Shared.Abstractions.Auth;

public record JsonWebToken(string AccessToken, long Expires, string Id, string Role, 
    IDictionary<string, IEnumerable<string>> Claims);