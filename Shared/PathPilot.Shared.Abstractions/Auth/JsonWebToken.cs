namespace PathPilot.Shared.Abstractions.Auth;

public sealed class JsonWebToken(
    string accessToken,
    long expires,
    string id,
    string role,
    IDictionary<string, IEnumerable<string>> claims)
{
    public string AccessToken { get; } = accessToken;
    public long Expires { get; } = expires;
    public string Id { get; } = id;
    public string Role { get; } = role;
    public IDictionary<string, IEnumerable<string>> Claims { get; } = claims;
    public string Email { get; set; }
    public string FullName { get; set; }
}