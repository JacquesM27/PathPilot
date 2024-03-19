using PathPilot.Modules.Users.Core.DAL.Mappings;
using PathPilot.Modules.Users.Tests.Unit.Helpers;
using Shouldly;

namespace PathPilot.Modules.Users.Tests.Unit.Core.MongoDbMappings;

public class UserMappingsTests
{
    [Fact]
    public void ToDocument_ShouldMapUserToUserDocument()
    {
        // Arrange
        var user = UserHelper.GetUser();

        // Act
        var userDocument = user.ToDocument();

        // Assert
        userDocument.Id.ShouldBe(user.Id.Value);
        userDocument.FirstName.ShouldBe(user.Name.FirstName);
        userDocument.LastName.ShouldBe(user.Name.LastName);
        userDocument.Email.ShouldBe(user.Email);
        userDocument.Password.ShouldBe(user.Password);
        userDocument.Role.ShouldBe("user");
        userDocument.Role.ShouldBe(user.Role);
        userDocument.IsActive.ShouldBe(user.IsActive);
        userDocument.Claims.ShouldBe(user.Claims);
    }
    
    [Fact]
    public void ToDocument_ShouldMapUserToUserDocument_WithAdminRole()
    {
        // Arrange
        var user = UserHelper.GetAdmin();

        // Act
        var userDocument = user.ToDocument();

        // Assert
        userDocument.Id.ShouldBe(user.Id.Value);
        userDocument.FirstName.ShouldBe(user.Name.FirstName);
        userDocument.LastName.ShouldBe(user.Name.LastName);
        userDocument.Email.ShouldBe(user.Email);
        userDocument.Password.ShouldBe(user.Password);
        userDocument.Role.ShouldBe("admin");
        userDocument.Role.ShouldBe(user.Role);
        userDocument.IsActive.ShouldBe(user.IsActive);
        userDocument.Claims.ShouldBe(user.Claims);
    }

    [Fact]
    public void FromDocument_ShouldMapUserDocumentToUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var email = "test@example.com";
        var password = "this is some hash";
        var role = "user";
        var isActive = true;
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            {"permissions", new List<string>{"trips", "users"}},
            {"OtherClaim", new List<string>{"value1", "value2"}}
        };
        var userDocument = new UserDocument
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            Role = role,
            IsActive = isActive,
            Claims = claims
        };

        // Act
        var user = userDocument.FromDocument();

        // Assert
        user.Id.Value.ShouldBe(userId);
        user.Name.FirstName.ShouldBe(firstName);
        user.Name.LastName.ShouldBe(lastName);
        user.Email.Value.ShouldBe(email);
        user.Password.Value.ShouldBe(password);
        user.Role.ShouldBe(role);
        user.IsActive.ShouldBe(isActive);
        user.Claims.ShouldBe(claims);
    }
}