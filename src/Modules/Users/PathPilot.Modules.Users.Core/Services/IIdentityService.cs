using PathPilot.Modules.Users.Core.DTO;
using PathPilot.Shared.Abstractions.Auth;

namespace PathPilot.Modules.Users.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id);
    Task SignUpAsync(SignUpDto dto);
    Task<JsonWebToken> SignInAsync(SignInDto dto);
}