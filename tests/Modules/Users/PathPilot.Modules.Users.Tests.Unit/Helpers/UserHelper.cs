using Microsoft.AspNetCore.Identity;
using PathPilot.Modules.Users.Core.Entities;

namespace PathPilot.Modules.Users.Tests.Unit.Helpers;

public static class UserHelper
{
    private static PasswordHasher<User> _passwordHasher = new();
    public static string DefaultPassword = "SecretPassword123!"; 
    public static Dictionary<string, IEnumerable<string>> GetClaims()
    {
        return new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
    }
    
    public static User GetUser()
    {
        var hash = _passwordHasher.HashPassword(default, DefaultPassword);
        const string firstName = "Some";
        const string lastName = "Guy";
        const string email = "some.guy@pathpilot.io";
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
        
        return User.CreateUser(firstName, lastName, email, hash, claims);
    }
    
    public static User GetAdmin()
    {
        var hash = _passwordHasher.HashPassword(default, DefaultPassword);
        const string firstName = "Admin";
        const string lastName = "Guy";
        const string email = "admin.guy1@pathpilot.io";
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
        
        return User.CreateAdmin(firstName, lastName, email, hash, claims);
    }
}