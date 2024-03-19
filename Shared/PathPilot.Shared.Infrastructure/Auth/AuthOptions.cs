namespace PathPilot.Shared.Infrastructure.Auth;

public sealed class AuthOptions
{
    public bool AuthentictionDisabled { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string IssuerSigningKey { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Challenge { get; set; } = "Bearer";
    public string MetadataAddress { get; set; } = string.Empty;
    public bool SaveToken { get; set; } = true;
    public bool SaveSigninToken { get; set; }
    public bool RequireAudience { get; set; } = true;
    public bool RequireHttpsMetadata { get; set; } = true;
    public bool RequireExpirationTime { get; set; } = true;
    public bool RequireSignedTokens { get; set; } = true;
    public TimeSpan Expiry { get; set; }
    public string ValidAudience { get; set; } = string.Empty;
    public IEnumerable<string> ValidAudiences { get; set; } = [];
    public string ValidIssuer { get; set; } = string.Empty;
    public IEnumerable<string> ValidIssuers { get; set; } = [];
    public bool ValidateActor { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateTokenReplay { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool RefreshOnIssuerKeyNotFound { get; set; } = true;
    public bool IncludeErrorDetails { get; set; } = true;
    public string AuthenticationType { get; set; } = string.Empty;
    public string NameClaimType { get; set; } = string.Empty;
    public string RoleClaimType { get; set; } = string.Empty;
}