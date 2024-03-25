using Microsoft.Extensions.Time.Testing;
using PathPilot.Shared.Abstractions.Auth;
using PathPilot.Shared.Infrastructure.Auth;

namespace PathPilot.Shared.Tests;

public static class AuthHelper
{
    private static readonly IAuthManager AuthManager;

    static AuthHelper()
    {
         var options = OptionsHelper.GetOptions<AuthOptions>("auth");
         var fakeTimeProvider = new FakeTimeProvider();
         fakeTimeProvider.SetUtcNow(new DateTimeOffset(DateTime.UtcNow));
         fakeTimeProvider.SetLocalTimeZone(TimeZoneInfo.Local);
         AuthManager = new AuthManager(options, fakeTimeProvider);
    }

    public static string CreateJwt(string userId, string? role = null, string? audience = null,
        IDictionary<string, IEnumerable<string>>? claims = null) 
        => AuthManager.CreateToken(userId, role, audience, claims).AccessToken;
}