using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using PathPilot.Modules.Users.Core.DTO;
using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.Exceptions;
using PathPilot.Modules.Users.Core.Repositories;
using PathPilot.Modules.Users.Core.Services;
using PathPilot.Modules.Users.Tests.Unit.Helpers;
using PathPilot.Shared.Abstractions.Auth;

namespace PathPilot.Modules.Users.Tests.Unit.Core.Services;

public class IdentityServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly IdentityService _identityService;

    public IdentityServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher<User>>();
        _authManager = Substitute.For<IAuthManager>();
        _identityService = new IdentityService(_userRepository, _passwordHasher, _authManager);
    }

    [Fact]
    public async Task SignUpUserAsync_ShouldThrowEmailInUseException_WhenEmailIsAlreadyRegistered()
    {
        // Arrange
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            { "permissions", new List<string> { "trips", "users" } },
        };
        var signUpDto = new SignUpDto("john.doe@pp.com", "John", "Doe", "JohnDoe1234!", claims);
        var user = UserHelper.GetUser();
        _userRepository.GetAsync("john.doe@pp.com").Returns(user);

        // Act & Assert
        await Should.ThrowAsync<EmailInUseException>(async () => await _identityService.SignUpUserAsync(signUpDto));
        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task SignUpUserAsync_ShouldAddUserToRepository_WhenSignUpDtoIsValid()
    {
        // Arrange
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            { "permissions", new List<string> { "trips", "users" } },
        };
        var signUpDto = new SignUpDto("john.doe@pp.com", "John", "Doe", "JohnDoe1234!", claims);

        // Act
        await _identityService.SignUpUserAsync(signUpDto);

        // Assert
        await _userRepository.Received(1).AddAsync(Arg.Any<User>());
    }
    
    [Fact]
    public async Task SignUpAdminAsync_ShouldThrowEmailInUseException_WhenEmailIsAlreadyRegistered()
    {
        // Arrange
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            { "permissions", new List<string> { "trips", "users" } },
        };
        var signUpDto = new SignUpDto("john.doe@pp.com", "John", "Doe", "JohnDoe1234!", claims);
        var user = UserHelper.GetUser();
        _userRepository.GetAsync("john.doe@pp.com").Returns(user);

        // Act & Assert
        await Should.ThrowAsync<EmailInUseException>(async () => await _identityService.SignUpAdminAsync(signUpDto));
        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task SignUpAdminAsync_ShouldAddUserToRepository_WhenSignUpDtoIsValid()
    {
        // Arrange
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            { "permissions", new List<string> { "trips", "users" } },
        };
        var signUpDto = new SignUpDto("john.doe@pp.com", "John", "Doe", "JohnDoe1234!", claims);

        // Act
        await _identityService.SignUpAdminAsync(signUpDto);

        // Assert
        await _userRepository.Received(1).AddAsync(Arg.Any<User>());
    }

    [Fact]
    public async Task SignInAsync_ShouldThrowInvalidCredentialException_WhenUserDoesNotExist()
    {
        // Arrange
        var signInDto = new SignInDto("john.doe@pp.com", "JohnDoe1234!");
        _userRepository.GetAsync("john.doe@pp.com").Returns((User)null);

        // Act & Assert
        await Should.ThrowAsync<InvalidCredentialsException>(async () => await _identityService.SignInAsync(signInDto));
        _passwordHasher.DidNotReceive().VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), 
            Arg.Any<string>());
        _authManager.DidNotReceive().CreateToken(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<IDictionary<string, IEnumerable<string>>?>());
    }
    
    [Fact]
    public async Task SignInAsync_ShouldReturnJsonWebToken_WhenUserExistsAndCredentialsAreValid()
    {
        // Arrange
        var signInDto = new SignInDto("john.doe@pp.com", "JohnDoe1234!");
        var userPasswordHash = "hashed_password";
        var user = UserHelper.GetUser();
        _userRepository.GetAsync("john.doe@pp.com").Returns(user);
        _passwordHasher.VerifyHashedPassword(default, Arg.Any<string>(), Arg.Any<string>()).Returns(PasswordVerificationResult.Success);
        _authManager.CreateToken(user.Id.Value.ToString(), user.Role, claims: user.Claims).Returns(new JsonWebToken(
            accessToken: "test_access_token",
            expires: 1234567890,
            id: user.Id.Value.ToString(),
            role: user.Role,
            claims: user.Claims)
        {
            Email = user.Email,
            FullName = user.Name.ToString()
        });

        // Act
        var result = await _identityService.SignInAsync(signInDto);

        // Assert
        result.ShouldNotBeNull();
        result.AccessToken.ShouldBe("test_access_token");
        result.Expires.ShouldBe(1234567890);
        result.Id.ShouldBe(user.Id.Value.ToString());
        result.Role.ShouldBe(user.Role);
        result.Claims.ShouldBe(user.Claims);
        result.Email.ShouldBe(user.Email);
        result.FullName.ShouldBe(user.Name.ToString());
        _passwordHasher.Received(1).VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), 
            Arg.Any<string>());
        _authManager.Received(1).CreateToken(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<IDictionary<string, IEnumerable<string>>?>());
    }
}

