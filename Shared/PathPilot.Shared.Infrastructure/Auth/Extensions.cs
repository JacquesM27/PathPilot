using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PathPilot.Shared.Abstractions.Auth;
using PathPilot.Shared.Abstractions.Modules;
using PathPilot.Shared.Infrastructure.Options;

namespace PathPilot.Shared.Infrastructure.Auth;

internal static class Extensions
{
    internal static IServiceCollection AddAuth(this IServiceCollection services,
        IEnumerable<IModule>? modules = null, Action<JwtBearerOptions>? optionsFactory = null)
    {
        var options = services.GetOptions<AuthOptions>("auth");
        services.AddSingleton<IAuthManager, AuthManager>();

        if (options.AuthentictionDisabled)
            services.AddSingleton<IPolicyEvaluator, DisabledAuthenticationPolicyEvaluator>();
        
        var tokenValidationParameters = new TokenValidationParameters()
        {
            RequireAudience = options.RequireAudience,
            ValidIssuer = options.ValidIssuer,
            ValidIssuers = options.ValidIssuers,
            ValidateActor = options.ValidateActor,
            ValidAudience = options.ValidAudience,
            ValidAudiences = options.ValidAudiences,
            ValidateAudience = options.ValidateAudience,
            ValidateIssuer = options.ValidateIssuer,
            ValidateLifetime = options.ValidateLifetime,
            ValidateTokenReplay = options.ValidateTokenReplay,
            ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
            SaveSigninToken = options.SaveSigninToken,
            RequireExpirationTime = options.RequireExpirationTime,
            RequireSignedTokens = options.RequireSignedTokens,
            ClockSkew = TimeSpan.Zero
        };

        if (string.IsNullOrWhiteSpace(options.IssuerSigningKey))
            throw new ArgumentException("Missing issuer signing key.", nameof(options.IssuerSigningKey));

        if (!string.IsNullOrWhiteSpace(options.AuthenticationType))
            tokenValidationParameters.AuthenticationType = options.AuthenticationType;

        var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);

        if (!string.IsNullOrWhiteSpace(options.NameClaimType))
            tokenValidationParameters.NameClaimType = options.NameClaimType;

        if (!string.IsNullOrWhiteSpace(options.RoleClaimType))
            tokenValidationParameters.RoleClaimType = options.RoleClaimType;

        services
            .AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.Authority = options.Authority;
                jwtOptions.Audience = options.Audience;
                jwtOptions.MetadataAddress = options.MetadataAddress;
                jwtOptions.SaveToken = options.SaveToken;
                jwtOptions.RefreshOnIssuerKeyNotFound = options.RefreshOnIssuerKeyNotFound;
                jwtOptions.RequireHttpsMetadata = options.RequireHttpsMetadata;
                jwtOptions.IncludeErrorDetails = options.IncludeErrorDetails;
                jwtOptions.TokenValidationParameters = tokenValidationParameters;
                if (!string.IsNullOrWhiteSpace(options.Challenge))
                    options.Challenge = options.Challenge;
                optionsFactory?.Invoke(jwtOptions);
            });

        services.AddSingleton(options);
        services.AddSingleton(tokenValidationParameters);

        var policies = modules?.SelectMany(module => module.Policies ?? Enumerable.Empty<string>())
                       ?? Enumerable.Empty<string>();
        
        foreach (var policy in policies)
        {
            services
                .AddAuthorizationBuilder()
                .AddPolicy(policy, configurePolicy => configurePolicy.RequireClaim("permissions", policy));
        }
        
        return services;
    }
}