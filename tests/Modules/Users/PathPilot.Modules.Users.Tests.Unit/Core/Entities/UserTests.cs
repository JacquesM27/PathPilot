using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.Exceptions;
using Shouldly;

namespace PathPilot.Modules.Users.Tests.Unit.Core.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_WithValidData_ShouldSucceed()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var password = "important hash of password";
        var claims = new Dictionary<string, IEnumerable<string>>();

        // Act
        var user = User.CreateUser(firstName, lastName, email, password, claims);

        // Assert
        user.ShouldNotBeNull();
        user.Name.FirstName.ShouldBe(firstName);
        user.Name.LastName.ShouldBe(lastName);
        user.Email.Value.ShouldBe(email);
        user.Password.Value.ShouldBe(password);
        user.Role.ShouldBe("user");
        user.IsActive.ShouldBeTrue();
        user.Claims.ShouldBe(claims);
    }

    [Fact]
    public void CreateUser_WithEmptyFirstName_ShouldThrowEmptyFirstNameException()
    {
        // Arrange
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var password = "Password123";
        var claims = new Dictionary<string, IEnumerable<string>>();

        // Act & Assert
        Should.Throw<EmptyFirstNameException>(() => User.CreateUser("", lastName, email, password, claims));
    }

    [Fact]
    public void CreateUser_WithEmptyLastName_ShouldThrowEmptyLastNameException()
    {
        // Arrange
        var firstName = "John";
        var email = "john.doe@example.com";
        var password = "Password123";
        var claims = new Dictionary<string, IEnumerable<string>>();

        // Act & Assert
        Should.Throw<EmptyLastNameException>(() => User.CreateUser(firstName, "", email, password, claims));
    }

    [Fact]
    public void CreateUser_WithInvalidEmailAddress_ShouldThrowInvalidEmailAddressException()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "invalid-email";
        var password = "Password123";
        var claims = new Dictionary<string, IEnumerable<string>>();

        // Act & Assert
        Should.Throw<InvalidEmailAddressException>(() => User.CreateUser(firstName, lastName, email, password, claims));
    }

    [Fact]
    public void CreateAdmin_WithValidData_ShouldSucceed()
    {
        // Arrange
        var firstName = "Admin";
        var lastName = "User";
        var email = "admin@example.com";
        var password = "some hash1234";
        var claims = new Dictionary<string, IEnumerable<string>>();

        // Act
        var admin = User.CreateAdmin(firstName, lastName, email, password, claims);

        // Assert
        admin.ShouldNotBeNull();
        admin.Name.FirstName.ShouldBe(firstName);
        admin.Name.LastName.ShouldBe(lastName);
        admin.Email.Value.ShouldBe(email);
        admin.Password.Value.ShouldBe(password);
        admin.Role.ShouldBe("admin");
        admin.IsActive.ShouldBeTrue();
        admin.Claims.ShouldBe(claims);
    }
}