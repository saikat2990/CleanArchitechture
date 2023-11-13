namespace CleanArchitechture.Core.Interfaces.Services;

public interface ICurrentUserService
{
    string UserId { get; }
    string UserName { get; }
    string Email { get; }
    string Role { get; }
}