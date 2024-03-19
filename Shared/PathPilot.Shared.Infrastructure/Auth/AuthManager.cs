using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PathPilot.Shared.Abstractions.Auth;

namespace PathPilot.Shared.Infrastructure.Auth;

public sealed class AuthManager : IAuthManager
{
    private static readonly Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly AuthOptions _options;
    private readonly TimeProvider _timeProvider;
    private readonly SigningCredentials _signingCredentials;
    private readonly string _issuer;

    public AuthManager(AuthOptions options, TimeProvider timeProvider)
    {
        if (string.IsNullOrWhiteSpace(options.IssuerSigningKey))
            throw new InvalidOperationException("Issuer signing key is not set.");

        _options = options;
        _timeProvider = timeProvider;
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey)),
            SecurityAlgorithms.HmacSha512);
        _issuer = options.Issuer;
    }
    
    public JsonWebToken CreateToken(string userId, string? role = null, string? audience = null, 
        IDictionary<string, IEnumerable<string>>? claims = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID claim (subject) cannot be empty", nameof(userId));

        var now = _timeProvider.GetUtcNow();
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeMilliseconds().ToString())
        };
        
        if (!string.IsNullOrWhiteSpace(role))
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        
        if (!string.IsNullOrWhiteSpace(audience))
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (key, values) in claims)
            {
                customClaims.AddRange(values.Select(value => new Claim(key, value)));
            }
            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_options.Expiry);

        var jwt = new JwtSecurityToken(
            issuer: _issuer,
            claims: jwtClaims,
            notBefore: now.DateTime,
            expires: expires.DateTime,
            signingCredentials: _signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken(token, expires.ToUnixTimeMilliseconds(), userId, role ?? string.Empty,
            claims ?? EmptyClaims);
    }
}