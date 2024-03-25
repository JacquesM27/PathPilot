using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Time.Testing;
using PathPilot.Modules.Users.Core.DAL.Repositories;
using PathPilot.Modules.Users.Core.DTO;
using PathPilot.Modules.Users.Core.Repositories;
using PathPilot.Modules.Users.Tests.Integration.Common;
using PathPilot.Modules.Users.Tests.Unit.Helpers;
using PathPilot.Shared.Abstractions.Auth;
using PathPilot.Shared.Tests;

namespace PathPilot.Modules.Users.Tests.Integration.Controllers;

[Collection("Serialize3")]
public class AccountControllerTests:
    IClassFixture<TestApplicationFactory>,
    IClassFixture<TestUsersMongoContext>
{
    private const string path = "users-module/account";
    private readonly HttpClient _client;
    private readonly IUserRepository _userRepository;
    private readonly FakeTimeProvider _fakeTimeProvider = new();

    public AccountControllerTests(TestApplicationFactory factory, TestUsersMongoContext context)
    {
        _client = factory.CreateClient();
        _userRepository = new UserRepository(context.Context);
        _fakeTimeProvider.SetUtcNow(new DateTimeOffset(DateTime.UtcNow));
        _fakeTimeProvider.SetLocalTimeZone(TimeZoneInfo.Local);
    }

    [Fact]
    public async Task GetAsync_WithoutAuthorizedUser_ShouldReturnUnauthorized()
    {
        // Arrange
        // Act
        var response = await _client.GetAsync($"{path}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAsync_WithAuthorizedUser_ShouldReturnOkWithAccountDetails()
    {
        // Arrange
        var user = UserHelper.GetUser();
        await _userRepository.AddAsync(user);
        Authenticate(user.Id);

        // Act
        var response = await _client.GetAsync($"{path}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var accountDto = await response.Content.ReadFromJsonAsync<AccountDto>();
        accountDto.ShouldNotBeNull();
        accountDto.Id.ShouldBe(user.Id.Value);
        foreach (var accountDtoClaim in accountDto.Claims)
        {
            user.Claims.TryGetValue(accountDtoClaim.Key, out var claims);
            claims.ShouldNotBeNull();
            var equality = accountDtoClaim.Value.SequenceEqual(claims);
            equality.ShouldBeTrue();
        }

        accountDto.Email.ShouldBe(user.Email.Value);
        accountDto.Role.ShouldBe(user.Role);
    }

    [Fact]
    public async Task SignUpUserAsync_ForInvalidEmail_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new SignUpDto("this.is.not.an.email", "John", "Doe",
            "SecretPassword123!", UserHelper.GetClaims());
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-up-user", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task SignUpUserAsync_ForValidEmail_ShouldReturnNoContent()
    {
        // Arrange
        var command = new SignUpDto("john.doe@pp.com", "John", "Doe",
            "SecretPassword123!", UserHelper.GetClaims());
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-up-user", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SignUpAdminAsync_ForValidEmail_ShouldReturnNoContent()
    {
        // Arrange
        var command = new SignUpDto("john.doe@pp.com", "John", "Doe",
            "SecretPassword123!", UserHelper.GetClaims());
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-up-admin", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SignInAsync_ForInvalidEmail_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new SignInDto(string.Empty, string.Empty);
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-in", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task SignInAsync_ForMissingUser_ShouldReturnBadRequest()
    {
        // Arrange
        var user = UserHelper.GetUser();
        await _userRepository.AddAsync(user);
        var command = new SignInDto("not.john.notdoe@notpp.com", string.Empty);
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-in", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task SignInAsync_ForValidUser_ShouldReturnOkWithToken()
    {
        // Arrange
        var user = UserHelper.GetUser();
        await _userRepository.AddAsync(user);
        var command = new SignInDto(user.Email, UserHelper.DefaultPassword);
        
        // Act
        var response = await _client.PostAsJsonAsync($"{path}/sign-in", command);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var jwt = await response.Content.ReadFromJsonAsync<JsonWebToken>();
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNull();
        jwt.FullName.ShouldBe(user.Name.ToString());
        jwt.Email.ShouldBe(user.Email.Value);
        jwt.Role.ShouldBe(user.Role);
        jwt.Id.ShouldBe(user.Id.Value.ToString());
        foreach (var accountDtoClaim in jwt.Claims)
        {
            user.Claims.TryGetValue(accountDtoClaim.Key, out var claims);
            claims.ShouldNotBeNull();
            var equality = accountDtoClaim.Value.SequenceEqual(claims);
            equality.ShouldBeTrue();
        }

        jwt.Expires.ShouldBeGreaterThan(_fakeTimeProvider.GetLocalNow().ToUnixTimeMilliseconds());
    }

    private void Authenticate(Guid userId)
    {
        var jwt = AuthHelper.CreateJwt(userId.ToString());
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
}