using CleanArchitechture.Core.Types;

namespace CleanArchitechture.Core.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResult> LoginAsync(LoginPayload request);
}