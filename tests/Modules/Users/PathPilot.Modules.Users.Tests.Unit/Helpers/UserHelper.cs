using PathPilot.Modules.Users.Core.Entities;

namespace PathPilot.Modules.Users.Tests.Unit.Helpers;

internal static class UserHelper
{
    internal static User GetUser()
    {
        const string firstName = "Some";
        const string lastName = "Guy";
        const string email = "some.guy@pathpilot.io";
        const string password = "this is hash";
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
        
        return User.CreateUser(firstName, lastName, email, password, claims);
    }
    
    internal static User GetAdmin()
    {
        const string firstName = "Admin";
        const string lastName = "Guy";
        const string email = "admin.guy1@pathpilot.io";
        const string password = "this is hash";
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
        
        return User.CreateAdmin(firstName, lastName, email, password, claims);
    }
}