using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using PathPilot.Modules.Users.Core.DTO;
using PathPilot.Modules.Users.Core.Entities;
using PathPilot.Modules.Users.Core.Exceptions;
using PathPilot.Modules.Users.Core.Repositories;
using PathPilot.Shared.Abstractions.Auth;

namespace PathPilot.Modules.Users.Core.Services;

internal sealed class IdentityService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IAuthManager authManager) : IIdentityService
{
    public async Task<AccountDto?> GetAsync(Guid id)
    {
        var user = await userRepository.GetAsync(id);

        return user is null
            ? null
            : new AccountDto(user.Id, user.Email, user.Role, user.Claims);
    }

    public async Task SignUpUserAsync(SignUpDto dto)
    {
        ValidPassword(dto.Password);
        await ValidEmail(dto.Email);

        var password = passwordHasher.HashPassword(default, dto.Password);
        var user = User.CreateUser(dto.FirstName, dto.LastName, dto.Email, password, dto.Claims);
        await userRepository.AddAsync(user);
    }

    public async Task SignUpAdminAsync(SignUpDto dto)
    {
        ValidPassword(dto.Password);
        await ValidEmail(dto.Email);

        var password = passwordHasher.HashPassword(default, dto.Password);
        var user = User.CreateAdmin(dto.FirstName, dto.LastName, dto.Email, password, dto.Claims);
        await userRepository.AddAsync(user);
    }
    
    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var user = await userRepository.GetAsync(dto.Email.ToLowerInvariant())
                   ?? throw new InvalidCredentialException();

        if (passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password)
            == PasswordVerificationResult.Failed)
            throw new InvalidCredentialException();

        if (!user.IsActive)
            throw new UserNotActiveException(user.Id);

        var jwt = authManager.CreateToken(user.Id.Value.ToString(), user.Role, claims: user.Claims);
        jwt.Email = user.Email;
        jwt.FullName = user.Name.ToString();

        return jwt;
    }

    private async Task ValidEmail(string email)
    {
        var user = await userRepository.GetAsync(email);

        if (user is not null)
            throw new EmailInUseException();
    }

    private void ValidPassword(string password)
    {
         if (!HasPasswordValidPolicy(password))
            throw new InvalidPasswordException();
    }
    
    private static bool HasPasswordValidPolicy(string password)
    {
        if (password.Length < 6)
            return false;

        return password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any(IsSpecialCharacter);
    }

    private static bool IsSpecialCharacter(char c)
    {
        return "!@#$%^&*()_+-=[]{};':\"\\|,.<>?".Contains(c);
    }
}